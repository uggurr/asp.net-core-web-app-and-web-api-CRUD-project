let uploadedPhotos = [];
let deletePhotos = [];
var max = 4;
let flag = 0;
var count = 0;
var count2 = 0;
var deletedSchool = [];
var addedSchool = [];
var schools = [];
function TextVisibility() {
    if (uploadedPhotos.length > 0 || flag > 0) {
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
function btnVisibilty() {
    var deleteButtons = document.querySelectorAll('.btn-danger');
    deleteButtons.forEach(function (button) {
        if (button.id !== 'btn_1') {
            if (count > 1) {
                button.style.display = 'block';
            } else {
                button.style.display = 'none';
            }
        }
    });
}
$(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);

    const id = urlParams.get('id');
    const name = urlParams.get('name');
    const surname = urlParams.get('surname');
    const bday = urlParams.get('bday');
    const gender = urlParams.get('gender');
    const country = urlParams.get('county');
    const city = urlParams.get($.City);
    const explanation = urlParams.get('explanation');
    const photo = urlParams.get('photo');
    
    
    $.ajax({
        url: "/EmployeeEgitim/GetEmployeeEgitim",
        type: "GET",
        dataType: "json",
        data: { id: id },
        success: function (data) {
            count = data.length;
            count2 = data.length;
            document.getElementById('mezuniyet_1').value = data[0].mezuniyetYili;
            var dropdownId = 'okul_1';
            var dropdownVal = data[0].paramOkullarId;
            dropDown('#' + dropdownId, dropdownVal);
            //gelen dataya göre ekranda alan oluşturulacak.
            //oluşturulan alan silinirse db den silinecek.
            //yeni alan eklenirse o alanın bilgileri db ye kaydedilecek
            // HTML içeriği oluşturulacak container
            var container = $('#container');

            for (var i = 2; i <= count; i++) {
                var newHtml = `
                <div id="okulAlan_${i}" class="form-group row mb-5">
                    <div class="col-11 mb-4 border rounded">
                        <div class="row mb-4 mt-2">
                            <div class="col-sm-6">
                                <label for="okul" class="col-sm-12 col-form-label"><p class="text-sm-center">Mezun Olduğunuz Okul</p></label>
                                <div class="col-sm-12 container-fluid">
                                    <select class="form-control" id="okul_${i}">
                                    </select>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <label for="mezuniyet" class="col-sm-12 col-form-label"><p class="text-sm-center">Mezuniyet Yılı</p></label>
                                <div class="col-sm-12 container-fluid">
                                    <input type="text" class="form-control" id="mezuniyet_${i}" value="${data[i - 1].mezuniyetYili}">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-1">
                        <button type="button" class="btn btn-success btn-sm mb-2" onclick="cloneForm('okulAlan_${i}')">+</button>
                        <button type="button" id="btn_${i}" class="btn btn-danger btn-sm" onclick="deleteForm('okulAlan_${i}')" style="display: none;">x</button>

                    </div>
                </div>
            `;

                
                container.append(newHtml);
                btnVisibilty();
                dropdownId = 'okul_' + i;
                dropdownVal = data[i - 1].paramOkullarId;
                dropDown('#' + dropdownId, dropdownVal);
            }
        }
    })
       

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/Photo/GetPhotoUrl",
        data: { id: id },
        success: function (data) {
            if (data && data.length > 0) {
                const imageContainer = $('#previewArea');
                data.forEach(function (item, index) {
                    const photoData = {
                        id: item.id,
                        medyaUrl: '/MedyaKutuphanesi/' + item.medyaUrl,
                        name: item.medyaAdi,
                    };

                    /*uploadedPhotos.push(photoData);*/

                    const img = $('<img class="previewImage" src="' + photoData.medyaUrl + '" style="max-width:75%; max-height: 50; margin-bottom: 10px; border-radius: 10px;">');
                    const deleteButton = $('<button class="btn btn-danger rounded-circle d-inline-block position-realtive top-0 end-0 translate-middle ms-4">X</button>');

                    const imageDiv = $('<div></div>').append(img).append(deleteButton);
                    imageContainer.append(imageDiv);

                    deleteButton.click(function () {
                        imageDiv.remove();
                        flag--;
                        const indexToRemove = uploadedPhotos.findIndex(photo => photo.id === photoData.id);
                        const deletePhoto = {
                            id:item.id,
                        }
                        deletePhotos.push(deletePhoto);
                        if (indexToRemove !== -1) {
                            uploadedPhotos.splice(indexToRemove, 1);
                        }
                        TextVisibility();
                    });
                    flag++;
                });
                TextVisibility();
            }
        }
    });

    date = new Date(bday);
    year = date.getFullYear();
    month = date.getMonth() + 1;
    dt = date.getDate();

    if (dt < 10) {
        dt = '0' + dt;
    }
    if (month < 10) {
        month = '0' + month;
    }

    if (gender == 'K') {
        document.getElementById('genderK').checked = true;
    }
    else if (gender == 'E') {
        document.getElementById('genderE').checked = true;
    }
    else if (gender == 'B') {
        document.getElementById('genderB').checked = true;
    }

    document.getElementById('ad').value = name;
    document.getElementById('soyad').value = surname;
    document.getElementById('birthday').value = year + '-' + month + '-' + dt;
    document.getElementById('ulke').value = country;
    document.getElementById('sehir').value = city;
    if (explanation == 'string' || explanation == 'null') {
        document.getElementById('aciklama').value = ' ';
    } else {
        document.getElementById('aciklama').value = explanation;
    }
    /*document.getElementById('previewImage').value = photo;*/
    document.getElementById('headerName').innerHTML = name;
    /*console.log(id, name, surname, bday, gender, country, city,explanation, photo);*/
})

function cloneForm(id) {
    count++;
    if (count <= max) {
        var original = document.getElementById('okulAlan_1');
        var clone = original.cloneNode(true);
        var copyId = parseInt(id.split('_')[1]);

        for (var i = copyId + 1; i < count; i++) {
            if (count == 4 && copyId == 1) {
                var currentElement2 = document.getElementById('okulAlan_' + (i + 1));
                var currentElement = document.getElementById('okulAlan_' + (i));
                if (currentElement2 && currentElement) {
                    currentElement2.id = 'okulAlan_' + (i + 2);
                    currentElement.id = 'okulAlan_' + (i + 1);
                    var selects = currentElement2.getElementsByTagName('select');
                    var selects2 = currentElement.getElementsByTagName('select');
                    for (var j = 0; j < selects.length; j++) {
                        selects[j].id = 'okul_' + (i + 2);
                    }
                    for (var j = 0; j < selects2.length; j++) {
                        selects2[j].id = 'okul_' + (i + 1);
                    }
                    var inputs = currentElement2.getElementsByTagName('input');
                    var inputs2 = currentElement.getElementsByTagName('input');
                    for (var j = 0; j < inputs.length; j++) {
                        inputs[j].id = 'mezuniyet_' + (i + 2);
                    }
                    for (var j = 0; j < inputs2.length; j++) {
                        inputs2[j].id = 'mezuniyet_' + (i + 1);
                    }
                    var copyButton = currentElement2.querySelector('.btn-success');
                    var copyButton2 = currentElement.querySelector('.btn-success');
                    copyButton.setAttribute('onclick', "cloneForm('okulAlan_" + (i + 2) + "')");
                    copyButton2.setAttribute('onclick', "cloneForm('okulAlan_" + (i + 1) + "')");

                    var deleteButton = currentElement2.querySelector('.btn-danger');
                    var deleteButton2 = currentElement.querySelector('.btn-danger');
                    deleteButton.id = 'btn_' + (i + 2);
                    deleteButton2.id = 'btn_' + (i + 1);
                    deleteButton.setAttribute('onclick', "deleteForm('okulAlan_" + (i + 2) + "')");
                    deleteButton2.setAttribute('onclick', "deleteForm('okulAlan_" + (i + 1) + "')");

                    break;
                }
            }
            else {
                var currentElement = document.getElementById('okulAlan_' + i);
                if (currentElement) {
                    currentElement.id = 'okulAlan_' + (i + 1);
                    var selects = currentElement.getElementsByTagName('select');
                    for (var j = 0; j < selects.length; j++) {
                        selects[j].id = 'okul_' + (i + 1);
                    }
                    var inputs = currentElement.getElementsByTagName('input');
                    for (var j = 0; j < inputs.length; j++) {
                        inputs[j].id = 'mezuniyet_' + (i + 1);
                    }
                    var copyButton = currentElement.querySelector('.btn-success');
                    copyButton.setAttribute('onclick', "cloneForm('okulAlan_" + (i + 1) + "')");

                    var deleteButton = currentElement.querySelector('.btn-danger');
                    deleteButton.id = 'btn_' + (i + 1);
                    deleteButton.setAttribute('onclick', "deleteForm('okulAlan_" + (i + 1) + "')");
                }
            }

        }
        copyId++;
        /*counter++;*/
        clone.id = 'okulAlan_' + copyId;

        var selects = clone.getElementsByTagName('select');
        for (var i = 0; i < selects.length; i++) {
            selects[i].id = 'okul_' + copyId;
        }

        var inputs = clone.getElementsByTagName('input');
        for (var i = 0; i < inputs.length; i++) {
            inputs[i].id = 'mezuniyet_' + copyId;
            inputs[i].value = '';
        }

        var copyButton = clone.querySelector('.btn-success');
        copyButton.setAttribute('onclick', "cloneForm('okulAlan_" + copyId + "')");

        var deleteButton = clone.querySelector('.btn-danger');
        deleteButton.id = 'btn_' + copyId;
        deleteButton.setAttribute('onclick', "deleteForm('okulAlan_" + copyId + "')");

        document.getElementById('okulAlan_' + (copyId - 1)).insertAdjacentElement('afterend', clone);
        btnVisibilty();
        dropdownId = 'okul_' + copyId;
        dropDown('#' + dropdownId);
    }
    else {
        alert("En fazla 4 adet Okul Bilgisi Ekleyebilirsiniz.");
        count--;
    }
    /*document.body.appendChild(clone);*/
}
function deleteForm(id) {
    var elementToDelete = document.getElementById(id);
    if (elementToDelete) {
        elementToDelete.remove();
        count--;

        //var deletedId = parseInt(id.split('_')[1]);
        
        //if (deletedId <= count2) {
        //    var school = elementToDelete.querySelector(`#okul_${id.split('_')[1]}`).value;
        //    var year = elementToDelete.querySelector(`#mezuniyet_${id.split('_')[1]}`).value;

        //    var element = {
        //        Id: 0,
        //        EmployeeId: 0,
        //        ParamOkullarId: school,
        //        MezuniyetYili: year
        //    };
        //    deletedSchool.push(element);
        //}
        
        for (var i = parseInt(id.split('_')[1]) + 1; i <= count + 1; i++) {
            var currentElement = document.getElementById('okulAlan_' + i);
            if (currentElement) {
                currentElement.id = 'okulAlan_' + (i - 1);
                var selects = currentElement.getElementsByTagName('select');
                for (var j = 0; j < selects.length; j++) {
                    selects[j].id = 'okul_' + (i - 1);
                }
                var inputs = currentElement.getElementsByTagName('input');
                for (var j = 0; j < inputs.length; j++) {
                    inputs[j].id = 'mezuniyet_' + (i - 1);
                }
                var copyButton = currentElement.querySelector('.btn-success');
                copyButton.setAttribute('onclick', "cloneForm('okulAlan_" + (i - 1) + "')");
                var deleteButton = currentElement.querySelector('.btn-danger');
                deleteButton.id = 'btn_' + (i - 1);
                deleteButton.setAttribute('onclick', "deleteForm('okulAlan_" + (i - 1) + "')");
            }
        }
        
        btnVisibilty();
    }
}
function update() {
    
    const formData = new FormData();
    $.each(uploadedPhotos, function (index, photo) {
        
            formData.append(`photo_${index}`, photo);
        
    });

    const urlParams = new URLSearchParams(window.location.search);
    const id = urlParams.get('id'); 4

    // Bütün veriyi al. Bütün veriyi sil. Yeni veriyi kaydet.
    //if (deletedSchool.length > 0) {
        
    //}
    for (var i = 1; i <= count; i++) {
        var schoolId = document.getElementById(`okulAlan_${i}`);
        var uploadedId = parseInt(schoolId.id.split('_')[1]);

        var schoolValue = schoolId.querySelector(`#okul_${uploadedId}`).value;
        var yearValue = schoolId.querySelector(`#mezuniyet_${uploadedId}`).value;

        var element = {
            Id: 0,
            EmployeeId: 0,
            ParamOkullarId: schoolValue,
            MezuniyetYili: yearValue
        };

        
        addedSchool.push(element);
        
    }
    if (addedSchool.length > 0) {
        $.ajax({
            url: "/ParamOkullar/DeleteSchool",
            type: "delete",
            data: {
                employeeId: id,
                /*schools: deletedSchool*/
            },
            success: function () {
                $.ajax({
                    url: "/ParamOkullar/PostSchool",
                    type: "POST",
                    dataType: "json",
                    data: {
                        employeeId: id,
                        schools: addedSchool,
                    },
                    success: function () {

                    }
                })
            }
        })
        
    }
    //if (schools.length > 0) {
    //    $.ajax({
    //        url: "/ParamOkullar/DeleteSchool",
    //        type: "delete",
    //        data: {
    //            employeeId: id,
    //            schools: schools
    //        },
    //        success: function () {
    //            $.ajax({
    //                url: "/ParamOkullar/UpdateSchool",
    //                type: "PUT",
    //                dataType: "json",
    //                data: {
    //                    employeeId: id,
    //                    schools: schools,
    //                },
    //                success: function () {

    //                }
    //            })
    //        }
    //    })
        
    //}
    if (deletePhotos.length > 0) {
        
        $.ajax({
            url: "/EmployeePhotos/DeletePhoto",
            type: "delete",
            data: {
                photoIds: deletePhotos.map(photo => photo.id),
            },
            success: function () {
                
            }
        })
    }
    if (uploadedPhotos.length > 0) {
        $.ajax({
            url: '/Photo/AddPhoto',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (photoIds) {
                $.ajax({
                    url: "/EmployeePhotos/AddEmpPhoto",
                    type: "post",
                    dataType: "json",
                    data: {
                        photoIds: photoIds,
                        employeeId: id,
                    },
                    success: function () {
                        
                    }
                });
                if ($("#sehir").val() && $("#sehir").val() != "0") {
                    let lastPhotoId = photoIds[photoIds.length - 1];
                    let newEmployee = {
                        Id: id,
                        Name: $("#ad").val(),
                        Surname: $("#soyad").val(),
                        Bday: $("#birthday").val(),
                        Gender: $("input[type='radio']:checked").val(),
                        /*Country: $("#ulke").val(),*/
                        City: $("#sehir").val(),
                        Explanation: $("#aciklama").val(),
                        Photo: lastPhotoId,
                        Deleted: "0",
                    }
                    $.ajax({
                        url: "/Employee/UpdateEmployee",
                        type: "put",
                        dataType: "json",
                        data: newEmployee,
                        success: function (data) {
                            $("#ad").val(data.name),
                                $("#soyad").val(data.surname),
                                $("#birthday").val(data.bday),
                                $("input[type='radio']:checked").val(data.gender),
                                /*$("#ulke").val(data.country),*/
                                $("#sehir").val(data.city),
                                $("#aciklama").val(data.explanation),
                                /*$("photoUpload").val(data.photo),*/

                                alert("Güncelleme işlemi Başarılı");
                            window.location.href = 'https://localhost:44310/Employee/EmployeeList';

                        }

                    });
                }
                else {
                    alert("ülke ve şehir alanları boş bırakılamaz!");
                }

            }
        })
    }
    else {
        if ($("#sehir").val() && $("#sehir").val() != "0") {
            
            let newEmployee = {
                Id: id,
                Name: $("#ad").val(),
                Surname: $("#soyad").val(),
                Bday: $("#birthday").val(),
                Gender: $("input[type='radio']:checked").val(),
                /*Country: $("#ulke").val(),*/
                City: $("#sehir").val(),
                Explanation: $("#aciklama").val(),
                /*Photo: lastPhotoId,*/
                Deleted: "0",
            }
            $.ajax({
                url: "/Employee/UpdateEmployee",
                type: "put",
                dataType: "json",
                data: newEmployee,
                success: function (data) {
                    $("#ad").val(data.name),
                        $("#soyad").val(data.surname),
                        $("#birthday").val(data.bday),
                        $("input[type='radio']:checked").val(data.gender),
                        /*$("#ulke").val(data.country),*/
                        $("#sehir").val(data.city),
                        $("#aciklama").val(data.explanation),
                        /*$("photoUpload").val(data.photo),*/

                        alert("Güncelleme işlemi Başarılı");
                    window.location.href = 'https://localhost:44310/Employee/EmployeeList';

                }

            });
        }
        else {
            alert("ülke ve şehir alanları boş bırakılamaz!");
        }
    }
    
    
    
}
function dropDown(dropdown, dropdownVal) {
    $.ajax({
        url: "/ParamOkullar/GetOkul",
        type: "GET",
        dataType: "json",
        success: function (data) {
            $(dropdown).empty();
            /*if (!urlParams.get('id'))*/
                $(dropdown).append($('<option></option>', { value: '0', text: 'Okul Seçin' }));
            $.each(data, function (index, item) {
                $(dropdown).append($('<option></option>').attr('value', item.id).text(item.okulAdi));
            });
            if (dropdownVal) {
                $(dropdown).val(dropdownVal);
            };
        }
    })
}