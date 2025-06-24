private static double VLength(double[] vector)
{
    return Math.Sqrt(vector[0] * vector[0] + vector[1] * vector[1] + vector[2] * vector[2]);
}

private static double Dot(double[] a, double[] b)
{
    return a.Zip(b, (x, y) => x * y).Sum();
}

private static double[] Cross(double[] a, double[] b)
{
    return new double[]
    {
        a[1] * b[2] - a[2] * b[1],
        a[2] * b[0] - a[0] * b[2],
        a[0] * b[1] - a[1] * b[0]
    };
}
