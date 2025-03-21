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

// Create product feature adding mechanism
$(document).ready(function () {
    $("#addCategoryBtn").click(function () {
        var categoryIndex = $(".category-group").length; // Определяем индекс новой категории
        var categoryHtml = `<div class="mb-3 category-group">
            <input type="text" class="form-control mb-2 category-name" name="Features[${categoryIndex}].Name" placeholder="Назва групи характеристик">
            <button type="button" class="btn btn-secondary addFeatureBtn">Додати характеристику</button>
            <div class="featureContainer mt-2"></div>
        </div>`;
        $("#categoryContainer").append(categoryHtml);
    });

    $(document).on("click", ".addFeatureBtn", function () {
        var categoryIndex = $(this).closest(".category-group").index(); // Получаем индекс категории
        var featureIndex = $(this).siblings(".featureContainer").children(".feature-group").length; // Индекс фичи
        var featureHtml = `<div class="feature-group">
            <input type="text" class="form-control mt-2" name="Features[${categoryIndex}].Features[${featureIndex}].Name" placeholder="Назва харктеристи">
            <input type="text" class="form-control mt-2" name="Features[${categoryIndex}].Features[${featureIndex}].Value" placeholder="Значення">
        </div>`;
        $(this).siblings(".featureContainer").append(featureHtml);
    });
});

// Add to cart mechanism
document.addEventListener('DOMContentLoaded', function () {
    document.addEventListener('click', function (e) {
        const buyButton = e.target.closest('.buy-button');
        if (!buyButton) return;

        e.stopPropagation();
        e.preventDefault();

        const productId = buyButton.getAttribute('data-product-id');
        console.log(productId);

        fetch('/Products/AddProductToCart', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ productId: productId })
        })
            .then(response => response.json())
            .then(data => {
                if (!data.success) {
                    alert('Під час додавання товару до кошика виникла помилка.');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Під час додавання товару до кошика виникла помилка.');
            });
    });
}); 
