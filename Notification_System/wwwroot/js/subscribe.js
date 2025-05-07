function toggleInputFields() {
    const selectedType = document.getElementById("selectedType").value;

    document.getElementById("emailInput").classList.add("hidden");
    document.getElementById("smsInput").classList.add("hidden");
    document.getElementById("pushInput").classList.add("hidden");

    if (selectedType === "Email") {
        document.getElementById("emailInput").classList.remove("hidden");
    } else if (selectedType === "SMS") {
        document.getElementById("smsInput").classList.remove("hidden");
    } else if (selectedType === "Push") {
        document.getElementById("pushInput").classList.remove("hidden");
    }
}

window.onload = function () {
    toggleInputFields();
};

function handleSubscribe(event) {
    event.preventDefault(); // Prevent actual form submission

    const name = document.getElementById("name").value.trim();
    const selectedType = document.getElementById("selectedType").value;
    let isValid = true;

    if (!name) {
        alert("Please enter your name.");
        isValid = false;
    }

    if (selectedType === "Email") {
        const email = document.getElementById("email").value.trim();
        const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        if (!email || !emailPattern.test(email)) {
            alert("Please enter a valid email address.");
            isValid = false;
        }
    }

    if (selectedType === "SMS") {
        const phoneNumber = document.getElementById("phoneNumber").value.trim();
        const phonePattern = /^[+0-9]{1,}[0-9]{3,}$/;
        if (!phoneNumber || !phonePattern.test(phoneNumber)) {
            alert("Please enter a valid phone number (e.g., +1234567890).");
            isValid = false;
        }
    }

    if (selectedType === "Push") {
        const pushId = document.getElementById("pushId").value.trim();
        if (!pushId) {
            alert("Please enter a valid Push ID.");
            isValid = false;
        }
    }

    if (isValid) {
        document.getElementById("subscribeForm").style.display = "none";
        document.getElementById("messageBox").style.display = "block"; // Use display, not classList
    }
}

function resetForm() {
    document.getElementById("messageBox").style.display = "none";
    document.getElementById("subscribeForm").style.display = "block";
    document.getElementById("subscribeForm").reset();
    toggleInputFields();
}
