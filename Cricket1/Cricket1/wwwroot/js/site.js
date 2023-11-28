// wwwroot/js/site.js

function toggleAnimation(element, className) {
    element.classList.add(className);
    setTimeout(() => {
        element.classList.remove(className);
    }, 1000);
}

function handleLoginResponse(data, loginMessage) {
    const message = data.success ? "Login successful!" : data.errorMessage || "Invalid credentials. Please try again.";
    const color = data.success ? "#007bff" : "#dc3545";
    const className = data.success ? "animate-success" : "animate-error";

    displayMessage(loginMessage, message, color, className);
}

function handleError(error, loginMessage) {
    console.error('Error:', error);
    displayMessage(loginMessage, "An error occurred. Please try again.", "#dc3545", "animate-error");
}

function displayMessage(element, message, color, className) {
    element.innerText = message;
    setDynamicColor(element, color);
    toggleAnimation(element, className);
}

function validateLogin() {
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;
    const loginMessage = document.getElementById("loginMessage");

    // ... rest of the code

    fetch('/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email, password }),
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Server response was not ok');
            }
            return response.json();
        })
        .then(data => handleLoginResponse(data, loginMessage))
        .catch(error => handleError(error, loginMessage));

    return false;
}

function setDynamicColor(element, color) {
    element.style.color = color;
}
