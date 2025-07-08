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
