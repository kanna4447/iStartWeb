//$(document).ready(function () {
//    GetCustomer();
//});


//function GetCustomer() {
//    $('#tblData').DataTable({
//        bprocessing: true,
//        serverSide: true,
       
//        bfilter: true,
//        ajax:{
//            url: '/HomePage/GetData',
//            type: 'Post',
//            dataType: 'json',
//        },

//            columns: [
            
//                { "data": "UserId", "name": "UserId", "autowidth": true },
//            { "data": "Name", "name": "Name", "autowidth": true },
//            { "data": "EmailID", "name": "EmailID", "autowidth": true },
//                { "data": "Status", "name": "Status", "autowidth": true },
                

//                {

//                    //render: function (data, type, row, meta) {
//                    //    return '<a href="#" class="btn btn-sm btn-primary m-1 p-1">Edit</a>'
                       
//                    //},
//                //render: function (data, type, row, meta) {
//                //    return '<a href="#" class="btn btn-sm btn-primary m-1 p-1">Edit</a>'
//                //}
//            }

//        ],
//            columnDefs: [
//            {

//                targets: [0],
//                searchable: false,
//            }
//        ]

//               });


//}
