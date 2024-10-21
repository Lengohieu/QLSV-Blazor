////using AntDesign.Charts;
//using AntDesign.Charts;
//using QLSV.Data;

//namespace QLSV.Pages
//{
//    public partial class ColumnChart
//    {
//        object[] data3 = new object[1000];
//        List<object> data2 = new List<object>();
//        protected override async Task OnInitializedAsync()
//        {
//            StudentService service = new StudentService();
//            var slGioi = service.GetSinhVienByHocLuc("Giỏi");
//            var slKha = service.GetSinhVienByHocLuc("Khá");
//            var slTB = service.GetSinhVienByHocLuc("Trung Bình");
//            var slYeu = service.GetSinhVienByHocLuc("Yếu");
//            int tong = slGioi + slKha + slTB + slYeu;
//            var check = Math.Ceiling((decimal)((slGioi * 100) / tong));
//            int G = (slGioi * 100) / tong;
//            int K = (slKha * 100) / tong;
//            int TB = (slTB * 100) / tong;
//            int Y = (slYeu * 100) / tong;
//            data2.Add(new
//            {
//                type = "Giỏi",
//                value = G
//            });

//            data2.Add(new
//            {
//                type = "Khá",
//                value = K
//            });

//            data2.Add(new
//            {
//                type = "Trung Bình",
//                value = TB
//            });

//            data2.Add(new
//            {
//                type = "Yếu",
//                value = Y
//            });
//            data3 = data2.ToArray();
//            await base.OnInitializedAsync();

//        }




//        readonly object[] data1 =
//    {
//        new
//        {
//            type = "Giỏi",
//            value = 25
//        },
//        new
//        {
//            type = "Khá",
//            value = 25
//        },
//        new
//        {
//            type = "Trung Bình",
//            value = 25
//        },
//         new
//        {
//            type = "Yếu",
//            value = 25
//        },

//    };

//        readonly PieConfig config1 = new PieConfig
//        {
//            ForceFit = true,
//            Title = new Title
//            {
//                Visible = true,
//                Text = "Multicolor Pie Chart"
//            },
//            Description = new Description
//            {
//                Visible = true,
//                Text = "Specify the color mapping field (colorField), and the pie slice will be displayed in different colors according to the field data. To specify the color, you need to configure the color as an array. \nWhen the pie chart label type is set to inner, the label will be displayed inside the slice. Set the offset value of the offset control label."
//            },
//            Radius = 0.8,
//            AngleField = "value",
//            ColorField = "type",
//            Label = new PieLabelConfig
//            {
//                Visible = true,
//                Type = "inner"
//            }
//        };

//    }
//}
