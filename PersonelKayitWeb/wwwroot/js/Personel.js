let uploadedPhotos = [];
var addedSchool = [];



function TextVisibility() {
    if (uploadedPhotos.length > 0) {
        $('#defaultText').hide();
    } else {
        $('#defaultText').show();
    }
}
$('#photoUpload').change(function (event) {
    const files = event.target.files;
    if (files.length > 0) {
        if (files.length + $('.image-preview').length > 3) {
            alert('En fazla 3 fotoğraf yükleyebilirsiniz.');
            return;
        }
        $.each(files, function (index, file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                const imageSrc = e.target.result;
                const imagePreview = $('<div class="image-preview"></div>').appendTo('#previewArea');
                $('<img class="previewImage" src="' + imageSrc + '" style="max-width:75%; max-height: 50; margin-bottom: 10px; border-radius: 10px;">').appendTo(imagePreview);
                $('<button class="btn btn-danger rounded-circle d-inline-block position-realtive top-0 end-0 translate-middle ms-4">X</button>').appendTo(imagePreview).click(function () {
                    imagePreview.remove();
                    uploadedPhotos.splice(index, 1);
                    TextVisibility();
                });
            };
            reader.readAsDataURL(file);
            uploadedPhotos.push(file);
        });
    }
    TextVisibility();
});
function kaydet() {
    for (var i = 1; i <= counter; i++) {
        var inputElement = document.getElementById('mezuniyet_' + i);
        var school = document.getElementById('okul_' + i);

        var year = inputElement.value;
        var selectedSchool = school.value;

        var element = {
            Id:0,
            EmployeeId:0,
            ParamOkullarId:selectedSchool,
            MezuniyetYili:year
        };
        addedSchool.push(element);
    };
    console.log(addedSchool);
    const formData = new FormData();
    $.each(uploadedPhotos, function (index, photo) {
        formData.append(`photo_${index}`, photo);
    });
    if (uploadedPhotos.length > 0) {
        $.ajax({
            url: '/Photo/AddPhoto',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (photoIds) {
                if ($("#sehir").val() && $("#sehir").val() != "0") {
                    let lastPhotoId = photoIds[photoIds.length - 1];
                    let employee = {
                        Name: $("#ad").val(),
                        Surname: $("#soyad").val(),
                        Bday: $("#birthday").val(),
                        Gender: $("input[type='radio']:checked").val(),
                        /*Country: $("#ulke").val(),*/
                        City: $("#sehir").val(),
                        Explanation: $("#aciklama").val(),
                        Photo: lastPhotoId,
                        Deleted: '0',
                    };
                    $.ajax({
                        type: "post",
                        dataType: "json",
                        url: "/Employee/AddEmployee",
                        data: employee,
                        success: function (employeeId) {
                            /*let result = jQuery.parseJSON(func);*/
                            alert("Personel Ekleme işlemi başarılı");
                            $.ajax({
                                url: "/ParamOkullar/PostSchool",
                                type: "POST",
                                dataType: "json",
                                data: {
                                    employeeId: employeeId,
                                    schools: addedSchool,
                                },
                                success: function () {
                                    alert("okul bilgileri ekleme işlemi tamam!");
                                }
                            })
                            $.ajax({
                                type: "POST",
                                dataType: "json",
                                url: "/EmployeePhotos/AddEmpPhoto",
                                data: {
                                    photoIds: photoIds,
                                    employeeId: employeeId,
                                },
                                success: function (data) {
                                    
                                }
                            })
                            window.location.href = 'https://localhost:44310/Employee/Employee';
                        }
                    });
                }
                else {
                    alert("Ülke ve şehir alanları boş bırakılamaz!");
                }
            },
        });
    }
    else {
        if ($("#sehir").val() && $("#sehir").val() != "0") {
            let employee = {
                Name: $("#ad").val(),
                Surname: $("#soyad").val(),
                Bday: $("#birthday").val(),
                Gender: $("input[type='radio']:checked").val(),
                /*Country: $("#ulke").val(),*/
                City: $("#sehir").val(),
                Explanation: $("#aciklama").val(),
                /*Photo: lastPhotoId,*/
                Deleted: '0',
            };
            $.ajax({
                type: "post",
                dataType: "json",
                url: "/Employee/AddEmployee",
                data: employee,
                success: function (employeeId) {
                    /*let result = jQuery.parseJSON(func);*/
                    alert("Personel Ekleme işlemi başarılı");
                    //$.ajax({
                    //    type: "POST",
                    //    dataType: "json",
                    //    url: "/EmployeePhotos/AddEmpPhoto",
                    //    data: {
                    //        photoIds: photoIds,
                    //        employeeId: employeeId,
                    //    },
                    //    success: function (data) {
                    //        alert("PErsonel MEdya Islemi Tamam!");
                    //    }
                    //})
                    $.ajax({
                        url: "/ParamOkullar/PostSchool",
                        type: "POST",
                        dataType: "json",
                        data: {
                            employeeId: employeeId,
                            schools: addedSchool,
                        },
                        success: function () {
                            alert("okul bilgileri ekleme işlemi tamam!");
                        }
                    })
                    window.location.href = 'https://localhost:44310/Employee/Employee';
                }
            });
        }
        else {
            alert("Ülke ve şehir alanları boş bırakılamaz!");
        }
    }
        
    
           
};
