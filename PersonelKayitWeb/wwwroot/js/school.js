var counter = 1;
var max = 4;
const urlParams = new URLSearchParams(window.location.search);
var deleteButtonCreated = false;
$(document).ready(function () {
    var dropdownId = 'okul_' + counter;
    /*var dropdown = document.getElementById(dropdownId);*/
    dropDown('#' + dropdownId);
})

function btnVisibilty() {
    var deleteButtons = document.querySelectorAll('.btn-danger');
    deleteButtons.forEach(function (button) {
        if (button.id !== 'btn_1') {
            if (counter > 1) {
                button.style.display = 'block';
            } else {
                button.style.display = 'none';
            }
        }
    });
}
function cloneForm(id) {
    if (counter < max) {
        var original = document.getElementById('okulAlan_1');
        var clone = original.cloneNode(true);
        var copyId = parseInt(id.split('_')[1]);
        
        for (var i = copyId + 1; i <= counter; i++) {
            if (counter == 3 && copyId == 1) {
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
        counter++;
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
    }
    else {
        alert("En fazla 4 adet Okul Bilgisi Ekleyebilirsiniz.");
    }
    /*document.body.appendChild(clone);*/
}

function deleteForm(id) {
    var elementToDelete = document.getElementById(id);
    if (elementToDelete) {
        elementToDelete.remove();
        counter--;

        for (var i = parseInt(id.split('_')[1]) + 1; i <= counter + 1; i++) {
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
function dropDown(dropdown) {
    $.ajax({
        url: "/ParamOkullar/GetOkul",
        type: "GET",
        dataType: "json",
        success: function (data) {
            $(dropdown).empty();
            if (!urlParams.get('id'))
                $(dropdown).append($('<option></option>', { value: '0', text: 'Okul Seçin' }));
            $.each(data, function (index, item) {
                $(dropdown).append($('<option></option>').attr('value', item.id).text(item.okulAdi));
            });
        }
    })
}