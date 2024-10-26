namespace _2_лаба
{
    public partial class MyMatrix
    {
        double[,] matrix { get; set; }

        private double? cachedDeterminant = null; //пишу так щоб було зрозуміло що
                                                  //дет. ще не обчислено  

        public MyMatrix()
        {
            matrix = new double[,] { };
        }


    }

}
