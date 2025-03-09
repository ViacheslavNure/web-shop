
onLikeButtonClick = function (e, productId) {
    e.stopPropagation();
    e.preventDefault();

    console.log('Liked Product ID:', productId);
}