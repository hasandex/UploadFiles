$(document).ready(function () {
    $('#imageCover').on('change', function () {
        $('.img-preview').attr('src', window.URL.createObjectURL(this.files[0]));
    });
});