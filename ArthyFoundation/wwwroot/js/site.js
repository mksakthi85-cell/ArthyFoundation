// Local mock data store matching the seeded EF records for immediate client-side testing
const preseededProofs = {
    "TXN101": {
        cause: "Child Education Support",
        amount: "₹3,000",
        donor: "Amit Sharma",
        date: "25th August 2026",
        img: "shiksha-education.jpg",
        description: "Uniform & notebook sets successfully handed over to daughters of single parents in West Tambaram slums."
    },
    "TXN102": {
        cause: "Grassroots Grocery & Grain Seva",
        amount: "₹1,000",
        donor: "Sneha Krishnan",
        date: "28th August 2026",
        img: "aahar-hunger.jpg",
        description: "Dry grocery food staples bags (including V.P. Silver grain) delivered directly to nomadic settlements."
    },
    "TXN103": {
        cause: "Waterproof Shelter & Tarps",
        amount: "₹4,000",
        donor: "Ravi Kumar",
        date: "1st September 2026",
        img: "sahaara-shelter.jpg",
        description: "Waterproof yellow tarpaulins, heavy ropes and warm bedding mats distributed to protect thatch houses from monsoon rains."
    },
    "TXN104": {
        cause: "Tribal Community Development",
        amount: "₹5,000",
        donor: "Rajesh G",
        date: "3rd September 2026",
        img: "jeevitha-community.jpg",
        description: "Integrated development aid camp. Provided clean drinking canisters and vocational tools to tribal nomad groups."
    }
};

function executeTrace() {
    const inputVal = document.getElementById("trackerInput").value.trim().toUpperCase();
    const placeholder = document.getElementById("trackerPlaceholder");
    const successBox = document.getElementById("trackerSuccessBox");

    // UI clean sweep
    placeholder.classList.add("d-none");
    successBox.classList.add("d-none");

    if (preseededProofs[inputVal]) {
        const item = preseededProofs[inputVal];
        document.getElementById("traceBadgeCause").innerText = item.cause;
        document.getElementById("traceBadgeAmount").innerText = item.amount;
        document.getElementById("traceDonorName").innerText = item.donor;
        document.getElementById("traceDate").innerText = item.date;
        document.getElementById("traceDescription").innerText = item.description;

        const imgElement = document.getElementById("traceImage");
        imgElement.src = "/images/" + item.img;
        imgElement.onerror = function() {
            this.src = "https://placehold.co/400x250/1e5e3a/ffffff?text=" + encodeURIComponent(item.cause);
        };

        successBox.classList.remove("d-none");
    } else {
        // Fallback for custom transaction IDs generated dynamically during runtime
        if (inputVal.startsWith("TXN")) {
            document.getElementById("traceBadgeCause").innerText = "Sponsorship Active";
            document.getElementById("traceBadgeAmount").innerText = "Amount Pending Verify";
            document.getElementById("traceDonorName").innerText = "Local Donor";
            document.getElementById("traceDate").innerText = "Just Now";
            document.getElementById("traceDescription").innerText = "Your transaction is securely recorded in SQLite database. Field workers are preparing the delivery of your package and photographic verification will be uploaded here within 24 hours.";
            document.getElementById("traceImage").src = "/images/logo.png";
            successBox.classList.remove("d-none");
        } else {
            placeholder.classList.remove("d-none");
            alert("No records found for Transaction ID: " + inputVal + ". Please make sure you are inputting active records (TXN101 to TXN104).");
        }
    }
}