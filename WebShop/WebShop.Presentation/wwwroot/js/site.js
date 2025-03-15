// Like button clicked handler
onLikeButtonClick = function (e, productId) {
    e.stopPropagation();
    e.preventDefault();
    console.log('Liked Product ID:', productId);
}

// Create product dropdown script
$(document).ready(function () {
    $(".dropdown-item").click(function () {
        var selectedText = $(this).attr("data-value");
        $("#selectedValue").val(selectedText);
    });
});

// Create product image preview script
$(document).ready(function () {
    $(".dropdown-item").click(function () {
        var selectedText = $(this).attr("data-value");
        $("#selectedValue").val(selectedText);
    });

    $("#productImage").change(function (event) {
        var input = event.target;
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $("#imagePreview").html('<img src="' + e.target.result + '" style="max-width: 100%;max-height: 100 %;" alt="Uploaded image">');
            }
            reader.readAsDataURL(input.files[0]);
        }
    });
});