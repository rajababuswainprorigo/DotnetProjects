using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
namespace Flight_Landing.Pages
{
    public class AirTrafficControl
    {

        class Program
        {
            static void Main(string[] args)
            {
                Console.Write("Enter the number of planes: ");
                if (int.TryParse(Console.ReadLine(), out int numberOfPlanes))
                {
                    PriorityQueue<Plane> regularPlanes = new PriorityQueue<Plane>((a, b) => a.LandingTime.CompareTo(b.LandingTime));
                    PriorityQueue<Plane> emergencyPlanes = new PriorityQueue<Plane>((a, b) => a.LandingTime.CompareTo(b.LandingTime));



                    for (int i = 1; i <= numberOfPlanes; i++)
                    {
                        Console.Write($"Is Plane {i} an emergency landing? (y/n): ");
                        bool isEmergency = Console.ReadLine().Trim().Equals("y", StringComparison.OrdinalIgnoreCase);
                        if (isEmergency)
                        {
                            emergencyPlanes.Enqueue(new Plane($"Emergency Plane {i}", GenerateRandomLandingTime()));
                        }
                        else
                        {
                            regularPlanes.Enqueue(new Plane($"Plane {i}", GenerateRandomLandingTime()));
                        }
                    }



                    List<Runway> runways = new List<Runway>
            {
                new Runway(),
                new Runway()
            };



                    List<Task> tasks = new List<Task>();



                    foreach (var queue in new[] { emergencyPlanes, regularPlanes })
                    {
                        foreach (Plane plane in queue)
                        {
                            Task task = Task.Run(() =>
                            {
                                Runway availableRunway = null;



                                while (availableRunway == null)
                                {
                                    lock (runways)
                                    {
                                        availableRunway = runways.FirstOrDefault(r => r.IsAvailable);
                                    }



                                    if (availableRunway == null)
                                    {
                                        Thread.Sleep(100); // Wait for a short time before checking for available runways again
                                    }
                                }



                                availableRunway.IsAvailable = false;
                                Console.WriteLine($"Plane {plane.Name} is landing on Runway {runways.IndexOf(availableRunway) + 1} with a landing time of {plane.LandingTime} seconds.");



                                int remainingTime = plane.LandingTime;
                                while (remainingTime > 0)
                                {
                                    Console.WriteLine($"Time remaining for Plane {plane.Name}: {remainingTime} seconds");
                                    Thread.Sleep(1000); // Sleep for 1 second
                                    remainingTime--;
                                }



                                availableRunway.IsAvailable = true;
                                Console.WriteLine($"Plane {plane.Name} has landed and cleared Runway {runways.IndexOf(availableRunway) + 1}");
                            });



                            tasks.Add(task);
                        }
                    }



                    Task.WhenAll(tasks).Wait();
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number of planes.");
                }
            }



            static int GenerateRandomLandingTime()
            {
                Random random = new Random();
                return random.Next(10, 21);
            }
        }



        class Plane
        {
            public string Name { get; }
            public int LandingTime { get; }



            public Plane(string name, int landingTime)
            {
                Name = name;
                LandingTime = landingTime;
            }
        }



        class Runway
        {
            public bool IsAvailable { get; set; }



            public Runway()
            {
                IsAvailable = true;
            }
        }



        public class PriorityQueue<T> : IEnumerable<T>
        {
            private List<T> data;
            private Comparison<T> comparison;



            public PriorityQueue(Comparison<T> comparison)
            {
                data = new List<T>();
                this.comparison = comparison;
            }



            public void Enqueue(T item)
            {
                data.Add(item);
                int i = data.Count - 1;
                while (i > 0)
                {
                    int j = (i - 1) / 2;
                    if (comparison(data[i], data[j]) >= 0)
                        break;
                    (data[i], data[j]) = (data[j], data[i]);
                    i = j;
                }
            }



            public T Dequeue()
            {
                if (data.Count == 0)
                    throw new InvalidOperationException("Queue is empty");
                int last = data.Count - 1;
                T frontItem = data[0];
                data[0] = data[last];
                data.RemoveAt(last);
                last--;
                int i = 0;
                while (true)
                {
                    int left = i * 2 + 1;
                    if (left > last)
                        break;
                    int right = left + 1;
                    if (right <= last && comparison(data[right], data[left]) < 0)
                        left = right;
                    if (comparison(data[i], data[left]) <= 0)
                        break;
                    (data[i], data[left]) = (data[left], data[i]);
                    i = left;
                }
                return frontItem;
            }



            public int Count
            {
                get { return data.Count; }
            }



            public IEnumerator<T> GetEnumerator()
            {
                return data.GetEnumerator();
            }



            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}