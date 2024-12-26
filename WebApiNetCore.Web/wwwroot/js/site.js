$(document).ready(function () {

    function ProductsModel(id, productName, productDescrip, categoryName, price, discount, quantity, createDate) {
        this.Id = id,
        this.ProductName = productName;
        this.ProductDescrip = productDescrip;
        this.CategoryName = categoryName;
        this.Price = price;
        this.Discount = discount;
        this.Quantity = quantity;
        this.CreateDate = createDate;
    }

    $('#jsform').submit(function (e) {
        e.preventDefault();
        //var data = { Param1: '', Param2: '' };
        var token = $('#token').val();
        $.ajax({
            url: 'http://localhost:5000/api/Products/GetProducts',
            type: 'GET',
            contentType: 'application/json',
            //data: JSON.stringify(data),
            headers: {
                'Authorization': 'Bearer ' + token
            },
            success: function (response) {
                const items = response.map(function (item) {
                    return new ProductsModel(
                        item.id,
                        item.productName,
                        item.productDescrip,
                        item.categoryName,
                        item.price,
                        item.discount,
                        item.quantity,
                        item.createDate
                    );
                });
                $('#response').html(getHtml(items));
            },
            error: function (xhr, status, error) {
                $('#response').html('Error: ' + error );
            }
        });
    });

    function getHtml(List) {
        let html = '<ul>';
        List.forEach(function (i) {
            html += `<li>
            ProductName: <b>${i.ProductName}</b><br>
            ProductDescrip: <b>${i.ProductDescrip}</b><br>
            CategoryName: <b>${i.CategoryName}</b><br>
            Price: <b>${i.Price}</b><br>
            Discount: <b>${i.Discount}</b><br>
            Quantity: <b>${i.Quantity}</b><br>
            CreateDate: <b>${i.CreateDate}</b><br>
            <hr></li>`;
        });
        html += '</ul>';
        return html;
    }

});