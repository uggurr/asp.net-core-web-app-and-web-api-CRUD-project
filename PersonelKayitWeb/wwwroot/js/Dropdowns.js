$(document).ready(function () {
    
    const urlParams = new URLSearchParams(window.location.search);
    $('#sehir').prop('disabled', true);
    $.ajax({
        url: "/Adress/GetAdress",
        type: "GET",
        dataType: "json",
        data: {UstId:0},
        success: function (data) {
            var dropdown = $('#ulke');
            dropdown.empty();
            if (!urlParams.get('id'))
                dropdown.append($('<option></option>', { value: '0', text: 'Ülke Seçin' }));
            $.each(data, function (index, item) {
                dropdown.append($('<option></option>').attr('value', item.id).text(item.tanim));
            });
            if (urlParams.get('id')) {
                $('#sehir').prop('disabled', false);
                dropdown.val(urlParams.get('county'));
                var selected = dropdown.val();
                $.ajax({
                    url: "/Adress/GetAdress",
                    type: "GET",
                    dataType: "json",
                    data: { UstId: selected },
                    success: function (data) {
                        var dropdown = $('#sehir');
                        dropdown.prop('disabled', false);
                        dropdown.empty();
                        if (!urlParams.get('id'))
                            dropdown.append($('<option></option>', { value: '0', text: 'Şehir Seçin' }));
                        $.each(data, function (index, item) {

                            dropdown.append($('<option></option>').attr('value', item.id).text(item.tanim));
                        });
                        $('#sehir').val(urlParams.get('city'));
                    }
                })

            }
        }
    });
})
$('#ulke').change(function () {
    var selected = parseInt($(this).val());
    const urlParams = new URLSearchParams(window.location.search);
    console.log('fşlsgsşlfkg' + selected);
    if (selected == 0 ) {
        $('#sehir').prop('disabled', true);
        
    }
    else {
        $.ajax({
            url: "/Adress/GetAdress",
            type: "GET",
            dataType: "json",
            data: { UstId: selected },
            success: function (data) {
                var dropdown = $('#sehir');
                dropdown.prop('disabled', false);
                dropdown.empty();
                dropdown.append($('<option></option>', { value: '0', text: 'Şehir Seçin' }));
                $.each(data, function (index, item) {

                    dropdown.append($('<option></option>').attr('value', item.id).text(item.tanim));
                });
            }
        })
    }
    
})