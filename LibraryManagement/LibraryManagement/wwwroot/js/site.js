function createNewBook(type, year, title) {
    if (!title || isNaN(year)) {
        alert("Invalid year & title parameters.");
        return;
    }

    const safeTitle = encodeURIComponent(title);
    window.location.href = `${type}/FormEdit/${year}/${safeTitle}`
}

function confirmDelete(title) {
    return confirm(`Delete book <<${title}>>?`);
}

document.addEventListener("DOMContentLoaded", () => {
    docuemnt.queryselectorAll(".stars label").forEach(label => {
        const input = label.querySelector("input");
        const star = label.querySelector(".star-select");

        label.addEventListener("click", () => {
            document.querySelectorAll(".star-select").forEach(s => s.classList.remove("star-selected"));
            star.classList.add("star-selected");
        })
    })
})
