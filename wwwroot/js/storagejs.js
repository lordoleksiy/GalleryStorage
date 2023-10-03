function showImageModal(imageUrl) {
    var modal = document.getElementById("imageModal");
    var img = document.getElementById("expandedImage");
    img.src = imageUrl;
    var aspectRatio = img.width / img.height;

    if (aspectRatio > 1) {
        img.style.width = "80%";
        img.style.height = "auto";
    } else {
        img.style.width = "auto";
        img.style.height = "80%";
    }
    modal.style.display = "block";
}

document.querySelector(".close").addEventListener("click", function () {
    var modal = document.getElementById("imageModal");
    modal.style.display = "none";
});

document.addEventListener("DOMContentLoaded", function () {
    var viewLinks = document.querySelectorAll(".view-photo-link");
    var deleteLinks = document.querySelectorAll(".delete-photo-link");

    viewLinks.forEach(function (link) {
        link.addEventListener("click", function (e) {
            e.preventDefault();
            var imageUrl = this.getAttribute("data-image-url");
            showImageModal(imageUrl);
        });
    });
});