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
        var categoryIndex = $(".category-group").length;
        var categoryHtml = `<div class="mb-3 category-group">
            <input type="text" class="form-control mb-2 category-name h3" name="Features[${categoryIndex}].Name" placeholder="Назва групи характеристик">
            <button type="button" class="btn btn-secondary addFeatureBtn">Додати характеристику</button>
            <div class="featureContainer mt-2"></div>
        </div>`;
        $("#categoryContainer").append(categoryHtml);
    });

    $(document).on("click", ".addFeatureBtn", function () {
        var categoryIndex = $(this).closest(".category-group").index(); // Получаем индекс категории
        var featureIndex = $(this).siblings(".featureContainer").children(".feature-group").length; // Индекс фичи
        var featureHtml = `<div class="feature-group border rounded-2 p-3 mb-1">
            <input type="text" class="form-control mt-2" name="Features[${categoryIndex}].Features[${featureIndex}].Name" placeholder="Назва харктеристи">
            <input type="text" class="form-control mt-2" name="Features[${categoryIndex}].Features[${featureIndex}].Value" placeholder="Значення">
        </div>`;
        $(this).siblings(".featureContainer").append(featureHtml);
    });
});

// Add to cart mechanism
document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.buy-button').forEach(button => {
        button.addEventListener('click', async function (e) {

            e.stopPropagation();
            e.preventDefault();

            const productId = this.getAttribute('data-product-id');

            try {
                const response = await fetch('/Cart/AddProductToCart', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({ productId })
                });

                const data = await response.json();
                if (data.success) {
                    updateCartItemCount(data.count);
                } else {
                    alert('Помилка при додаванні товару до кошика');
                }
            } catch (error) {
                console.error('Error:', error);
                alert('Помилка при додаванні товару до кошика');
            }
        });
    });
}); 

function updateCartItemCount(count) {
    const cartItemCount = document.getElementById('cartItemCount');
    if (count > 0) {
        cartItemCount.textContent = count;
        cartItemCount.style.display = 'block';
    } else {
        cartItemCount.style.display = 'none';
    }
}

document.addEventListener('DOMContentLoaded', async function () {
    try {
        const response = await fetch('/Cart/GetCartCount');
        const data = await response.json();
        if (data.success) {
            updateCartItemCount(data.count);
        }
    } catch (error) {
        console.error('Error fetching cart count:', error);
    }
}); 

// Highlite active category
document.addEventListener('DOMContentLoaded', function() {
    const categoryLinks = document.querySelectorAll('.category-link');
    const currentUrl = window.location.href;
    
    categoryLinks.forEach(link => {
        if (currentUrl.includes(link.getAttribute('href'))) {
            link.classList.add('active');
        }
    });
}); 
