public void CalculateXYZtoYBC(int bendXYZcount)
{
    double[,] planeNormals = new double[listXaxisMembers.Count - 2, 3];

    for (int i = 0; i < bendingcountTBcal; i++)
    {
        // Compute vectors BA and BC for the angle at point B (ABC)
        double[] V1 = {
            listXaxisMembers[i].Value - listXaxisMembers[i + 1].Value,
            listYaxisMembers[i].Value - listYaxisMembers[i + 1].Value,
            listZaxisMembers[i].Value - listZaxisMembers[i + 1].Value };

        double[] V2 = {
            listXaxisMembers[i + 2].Value - listXaxisMembers[i + 1].Value,
            listYaxisMembers[i + 2].Value - listYaxisMembers[i + 1].Value,
            listZaxisMembers[i + 2].Value - listZaxisMembers[i + 1].Value };

        double V1Length = VLength(V1);
        double V2Length = VLength(V2);

        // Compute the bending angle between V1 and V2 (in degrees)
        bendAngleXYZ[i] = (Math.PI - Math.Acos(Dot(V1, V2) / (V1Length * V2Length))) * 180 / Math.PI;

        // Calculate the normal vector of the plane formed by V1 and V2
        planeNormals[i, 0] = V1[1] * V2[2] - V1[2] * V2[1];
        planeNormals[i, 1] = V1[2] * V2[0] - V1[0] * V2[2];
        planeNormals[i, 2] = V1[0] * V2[1] - V1[1] * V2[0];

        if (i > 0)
        {
            // Adjust the straight length using the tangent of half of the bend angle
            double correction = firstviewposL[i] * Math.Tan((bendAngleXYZ[i] / 2) * Math.PI / 180);

            afterLengthXYZ[i]     -= correction;
            afterLengthXYZ[i + 1] = V2Length - correction;

            // Calculate the magnitude of previous and current plane normals
            double prevLength = VLength(new double[] { planeNormals[i - 1, 0], planeNormals[i - 1, 1], planeNormals[i - 1, 2] });
            double currLength = VLength(new double[] { planeNormals[i, 0], planeNormals[i, 1], planeNormals[i, 2] });

            // Determine the direction of rotation using cross product
            double[] cross = Cross(
                new double[] { planeNormals[i - 1, 0], planeNormals[i - 1, 1], planeNormals[i - 1, 2] },
                new double[] { planeNormals[i, 0], planeNormals[i, 1], planeNormals[i, 2] }
            );

            double sign = Math.Sign(Dot(V1, cross));

            // Compute rotation angle between planes (in degrees)
            rotAngleXYZ[i] = Math.Acos(Dot(
                new double[] { planeNormals[i - 1, 0], planeNormals[i - 1, 1], planeNormals[i - 1, 2] },
                new double[] { planeNormals[i, 0], planeNormals[i, 1], planeNormals[i, 2] }
            ) / (prevLength * currLength)) * sign * 180 / Math.PI;
        }
        else
        {
            // First bend case: no rotation angle
            double correction = firstviewposL[i] * Math.Tan((bendAngleXYZ[i] / 2) * Math.PI / 180);

            afterLengthXYZ[i]     = V1Length - correction;
            afterLengthXYZ[i + 1] = V2Length - correction;

            // Store the first straight length
            firstlengthCal = Convert.ToSingle(Math.Round(afterLengthXYZ[i], 3));
            FDeviceDouble.SetValueAsDouble("Settings.FirstLength", firstlengthCal);

            rotAngleXYZ[i] = 0;
        }
    }

    // Write calculated values into the corresponding member lists
    for (int i = 0; i < bendingcountTBcal; i++)
    {
        listBendingAngelMembers[i].Value   = bendAngleXYZ[i];
        listAfterLenghtMembers[i].Value    = afterLengthXYZ[i + 1];
        listRotationAngelMembers[i].Value  = rotAngleXYZ[i + 1];
    }
}
