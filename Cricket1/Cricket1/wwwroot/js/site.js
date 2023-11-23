function validateLogin() {
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var loginMessage = document.getElementById("loginMessage");

    // Check for empty email or password
    if (!email || !password) {
        loginMessage.innerText = "Email and password are required.";
        setDynamicColor(loginMessage, "#dc3545"); // Red color
        toggleAnimation(loginMessage, "animate-error");
        return;
    }

    // More robust email validation
    if (!isValidEmail(email)) {
        loginMessage.innerText = "Invalid email format.";
        setDynamicColor(loginMessage, "#dc3545"); // Red color
        toggleAnimation(loginMessage, "animate-error");
        return;
    }

    // Send credentials to the server
    fetch('/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json', // Update content type to 'application/json'
        },
        body: JSON.stringify({ email: email, password: password }), // Send data as JSON
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
                setDynamicColor(loginMessage, "#007bff"); // Blue color
                toggleAnimation(loginMessage, "animate-success");
            } else {
                // Invalid credentials or other errors
                loginMessage.innerText = data.errorMessage || "Invalid credentials. Please try again.";
                setDynamicColor(loginMessage, "#dc3545"); // Red color
                toggleAnimation(loginMessage, "animate-error");
            }
        })
        .catch(error => {
            console.error('Error:', error);
            loginMessage.innerText = "An error occurred. Please try again.";
            setDynamicColor(loginMessage, "#dc3545"); // Red color
            toggleAnimation(loginMessage, "animate-error");
        });
}

function isValidEmail(email) {
    // Basic email validation (you might want to use a more comprehensive approach)
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}
