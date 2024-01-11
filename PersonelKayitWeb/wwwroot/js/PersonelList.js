
    //$(document).ready(function () {
    //    $('#dataTable').DataTable({
    //        "processing": true,
    //        "serverSide": true,
    //        "ajax": {
    //            url: "/Employee/GetEmployee",
    //            type: "Get",
    //            dataType: "json",
    //            //// "paging": true,
    //            //// "pagingType": "full_numbers",
    //            //"processing": true,
    //            success: function (data) {
    //                // Başarılı bir şekilde veri alındığında buradaki kod çalışır
    //                console.log(data); // Örnek olarak dönen verileri konsola yazdırabilirsiniz
    //                table.clear().draw();
    //                table.rows.add(data).draw();
    //            },
    //            serverSide: 'true',
    //            dataSrc: 'data',
    //        },
    //        "columns": [
    //            { "data": 'id' },
    //            { "data": 'name' },
    //            { "data": 'surname' },
    //            { "data": 'bday' },
    //            { "data": 'gender' },
    //            { "data": 'country' },
    //            { "data": 'city' },
    //            { "data": 'explanation' },
    //            { "data": 'photo' }
    //        ]
    //    })

//});
$(document).ready(function () {
    $.ajax({
        url: "/Employee/GetEmployee",
        type: "Get",
        dataType: "json",
        success: function (data) {
            $("#dataTable").DataTable({
                data: data,
                columns: [
                    
                    { "data": 'name' },
                    { "data": 'surname' },
                    { "data": 'bday' },
                    { "data": 'gender' },
                    { "data": 'county' },
                    { "data": 'city' },
                    { "data": 'explanation' },
                    {
                        "data": 'photo', "render": function (data, type, row) {
                           
                            if (row.photo) {
                                return '<img src="/MedyaKutuphanesi/' + row.photo + '" style="max-width:100%; max-height: 70%; border-radius: 15px;">';
                            } else {
                                return '';
                            }
                        }
                    },
                    {
                        "data": 'id', "render": function (data, type, row) {
                            return '<a type="button" class="btn btn-warning btn-sm mx-2" href="EditPage?id=' + row.id + '&name=' + row.name + '&surname=' + row.surname + '&bday=' + row.bday + '&gender=' + row.gender + '&county=' + row.countryId + '&city=' + row.cityId + '&explanation=' + row.explanation + '">Düzenle</a><a type="button" class="btn btn-danger btn-sm mx-2 my-2" onclick="Delete('+row.id+')">Sil</a>';
                        }
                    },
                ],              
            })
        }
    })
    
})

function Delete(id) {
    $.ajax({
        url: "/Delete/UpdateDelete",
        type: "PUT",
        dataType: "json",
        data: { id: id },
        success: function () {
            alert("silme işlemi başarılı!");
            window.location.href = 'https://localhost:44310/Employee/EmployeeList';
        }
    })
}