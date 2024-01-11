const file = document.getElementById('photoUpload');
const text = document.getElementById('defaultText');
let imageCounter = 1;
window.uploadedImages = [];
file.addEventListener('change', function () {
    const files = event.target.files;
    const previewArea = document.getElementById('previewArea');

    [...this.files].forEach(file => {
        if (file.type.match('image/jpeg') || file.type.match('image/png')) {
            const reader = new FileReader();
            const newImage = new Image();
            const imageId = 'previewImage_' + imageCounter;

            
            reader.addEventListener('load', function () {
                const imageData = this.result;
                const imageArr = {
                    id: imageId,
                    name: file.name, 
                    path: this.result 
                };
                newImage.src = imageData;
                if (imageCounter >= 1)
                    text.style.display = "none";
                newImage.id = imageId;
                newImage.alt = "Yüklenen Fotoğraf";
                newImage.style.maxWidth = "75%";
                newImage.style.maxHeight = "50%";
                newImage.style.marginBottom = "10px";
                const deleteButton = document.createElement('button');
                deleteButton.innerHTML = 'X';
                deleteButton.className = 'btn btn-danger rounded-circle d-inline-block position-realtive top-0 end-0 translate-middle ms-4';
                deleteButton.style.fontSize = '1rem';
                deleteButton.addEventListener('click', function () {
                    const imageToRemove = document.getElementById(imageId);
                    if (imageToRemove) {
                        previewArea.removeChild(imageToRemove);
                        this.parentNode.removeChild(this);
                        imageCounter--;
                        window.uploadedImages = window.uploadedImages.filter(img => img.id !== imageId);
                    }
                    if (imageCounter == 1)
                        text.style.display = "block";
                    
                });
                previewArea.appendChild(newImage);
                previewArea.appendChild(deleteButton); 

                window.uploadedImages.push(imageArr);
                imageCounter++;
            });

            reader.readAsDataURL(file);
        }
    });
});
