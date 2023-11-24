// wwwroot/js/site.js

function toggleAnimation(element, className) {
    element.classList.add(className);
    setTimeout(() => {
        element.classList.remove(className);
    }, 1000);
}

function validateLogin() {
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var loginMessage = document.getElementById("loginMessage");

    // ... rest of the code

    fetch('/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email: email, password: password }),
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Server response was not ok');
            }
            return response.json();
        })
        .then(data => {
            if (data.success) {
                // User authentication successful
                loginMessage.innerText = "Login successful!";
                setDynamicColor(loginMessage, "#007bff");
                toggleAnimation(loginMessage, "animate-success");
            } else {
                // Invalid credentials or other errors
                loginMessage.innerText = data.errorMessage || "Invalid credentials. Please try again.";
                setDynamicColor(loginMessage, "#dc3545");
                toggleAnimation(loginMessage, "animate-error");
            }
        })
        .catch(error => {
            console.error('Error:', error);
            loginMessage.innerText = "An error occurred. Please try again.";
            setDynamicColor(loginMessage, "#dc3545");
            toggleAnimation(loginMessage, "animate-error");
        });
}

function setDynamicColor(element, color) {
    element.style.color = color;
}

function isValidEmail(email) {
    // Implement more robust email validation if needed
    return email.checkValidity();
}
