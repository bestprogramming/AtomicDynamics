﻿using System.Diagnostics;
using System.Numerics;

namespace AtomicDynamics
{
    public partial struct Big
    {
        public static readonly BigInteger[] Pow10s = new BigInteger[]
        {
            new(new byte[] { 1 }),
            new(new byte[] { 10 }),
            new(new byte[] { 100 }),
            new(new byte[] { 232, 3 }),
            new(new byte[] { 16, 39 }),
            new(new byte[] { 160, 134, 1 }),
            new(new byte[] { 64, 66, 15 }),
            new(new byte[] { 128, 150, 152, 0 }),
            new(new byte[] { 0, 225, 245, 5 }),
            new(new byte[] { 0, 202, 154, 59 }),
            new(new byte[] { 0, 228, 11, 84, 2 }),
            new(new byte[] { 0, 232, 118, 72, 23 }),
            new(new byte[] { 0, 16, 165, 212, 232, 0 }),
            new(new byte[] { 0, 160, 114, 78, 24, 9 }),
            new(new byte[] { 0, 64, 122, 16, 243, 90 }),
            new(new byte[] { 0, 128, 198, 164, 126, 141, 3 }),
            new(new byte[] { 0, 0, 193, 111, 242, 134, 35 }),
            new(new byte[] { 0, 0, 138, 93, 120, 69, 99, 1 }),
            new(new byte[] { 0, 0, 100, 167, 179, 182, 224, 13 }),
            new(new byte[] { 0, 0, 232, 137, 4, 35, 199, 138, 0 }),
            new(new byte[] { 0, 0, 16, 99, 45, 94, 199, 107, 5 }),
            new(new byte[] { 0, 0, 160, 222, 197, 173, 201, 53, 54 }),
            new(new byte[] { 0, 0, 64, 178, 186, 201, 224, 25, 30, 2 }),
            new(new byte[] { 0, 0, 128, 246, 74, 225, 199, 2, 45, 21 }),
            new(new byte[] { 0, 0, 0, 161, 237, 204, 206, 27, 194, 211, 0 }),
            new(new byte[] { 0, 0, 0, 74, 72, 1, 20, 22, 149, 69, 8 }),
            new(new byte[] { 0, 0, 0, 228, 210, 12, 200, 220, 210, 183, 82 }),
            new(new byte[] { 0, 0, 0, 232, 60, 128, 208, 159, 60, 46, 59, 3 }),
            new(new byte[] { 0, 0, 0, 16, 97, 2, 37, 62, 94, 206, 79, 32 }),
            new(new byte[] { 0, 0, 0, 160, 202, 23, 114, 109, 174, 15, 30, 67, 1 }),
            new(new byte[] { 0, 0, 0, 64, 234, 237, 116, 70, 208, 156, 44, 159, 12 }),
            new(new byte[] { 0, 0, 0, 128, 38, 75, 145, 192, 34, 32, 190, 55, 126 }),
            new(new byte[] { 0, 0, 0, 0, 129, 239, 172, 133, 91, 65, 109, 45, 238, 4 }),
            new(new byte[] { 0, 0, 0, 0, 10, 91, 193, 56, 147, 141, 68, 198, 77, 49 }),
            new(new byte[] { 0, 0, 0, 0, 100, 142, 141, 55, 192, 135, 173, 190, 9, 237, 1 }),
            new(new byte[] { 0, 0, 0, 0, 232, 143, 135, 43, 130, 77, 199, 114, 97, 66, 19 }),
            new(new byte[] { 0, 0, 0, 0, 16, 159, 75, 179, 21, 7, 201, 123, 206, 151, 192, 0 }),
            new(new byte[] { 0, 0, 0, 0, 160, 54, 244, 0, 217, 70, 218, 213, 16, 238, 133, 7 }),
            new(new byte[] { 0, 0, 0, 0, 64, 34, 138, 9, 122, 196, 134, 90, 168, 76, 59, 75 }),
            new(new byte[] { 0, 0, 0, 0, 128, 86, 101, 95, 196, 172, 67, 137, 147, 254, 80, 240, 2 }),
            new(new byte[] { 0, 0, 0, 0, 0, 97, 245, 185, 171, 191, 164, 92, 195, 241, 41, 99, 29 }),
            new(new byte[] { 0, 0, 0, 0, 0, 202, 149, 67, 181, 124, 111, 158, 161, 113, 163, 223, 37, 1 }),
            new(new byte[] { 0, 0, 0, 0, 0, 228, 217, 163, 20, 223, 90, 48, 80, 112, 98, 188, 122, 11 }),
            new(new byte[] { 0, 0, 0, 0, 0, 232, 130, 102, 206, 182, 140, 227, 33, 99, 216, 91, 203, 114 }),
            new(new byte[] { 0, 0, 0, 0, 0, 16, 29, 1, 16, 36, 127, 227, 82, 223, 115, 150, 241, 123, 4 }),
            new(new byte[] { 0, 0, 0, 0, 0, 160, 34, 11, 160, 104, 247, 226, 60, 185, 134, 224, 111, 215, 44 }),
            new(new byte[] { 0, 0, 0, 0, 0, 64, 90, 111, 64, 22, 170, 221, 96, 60, 67, 197, 94, 106, 192, 1 }),
            new(new byte[] { 0, 0, 0, 0, 0, 128, 134, 89, 132, 222, 164, 168, 200, 91, 160, 180, 179, 39, 132, 17 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 65, 127, 43, 177, 112, 150, 214, 149, 67, 14, 5, 141, 41, 175, 0 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 138, 248, 178, 235, 102, 224, 97, 218, 163, 142, 50, 130, 159, 215, 6 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 100, 181, 253, 52, 5, 196, 210, 135, 102, 146, 249, 21, 59, 108, 68 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 232, 21, 233, 17, 52, 168, 59, 78, 1, 184, 191, 219, 78, 58, 172, 2 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 16, 219, 26, 179, 8, 146, 84, 14, 13, 48, 125, 149, 20, 71, 186, 26 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 160, 142, 12, 255, 86, 180, 77, 143, 130, 224, 227, 214, 205, 198, 70, 11, 1 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 64, 146, 125, 246, 101, 11, 9, 153, 25, 197, 230, 100, 10, 196, 195, 112, 10 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 128, 182, 231, 160, 251, 113, 90, 250, 255, 178, 3, 241, 103, 168, 165, 103, 104 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 33, 13, 73, 212, 115, 136, 199, 255, 253, 36, 106, 15, 148, 120, 12, 20, 4 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 74, 131, 218, 74, 134, 84, 203, 253, 235, 113, 37, 154, 200, 181, 124, 200, 40 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 228, 32, 137, 236, 62, 77, 241, 233, 55, 115, 118, 5, 214, 25, 223, 212, 151, 1 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 232, 72, 91, 61, 117, 4, 109, 35, 47, 128, 160, 54, 92, 2, 183, 80, 238, 15 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 16, 217, 144, 101, 148, 44, 66, 98, 215, 1, 69, 34, 154, 23, 38, 39, 79, 159, 0 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 160, 122, 168, 247, 203, 189, 149, 214, 105, 18, 178, 86, 5, 236, 124, 135, 23, 57, 6 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 64, 202, 148, 172, 247, 105, 217, 97, 34, 184, 244, 98, 53, 56, 225, 74, 235, 58, 62 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 128, 230, 207, 189, 172, 35, 126, 210, 87, 49, 143, 221, 21, 50, 204, 236, 48, 77, 110, 2 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 31, 106, 191, 100, 237, 56, 110, 237, 151, 167, 218, 244, 249, 63, 233, 3, 79, 24 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 10, 54, 37, 122, 239, 69, 57, 78, 70, 239, 139, 138, 144, 195, 127, 28, 39, 22, 243, 0 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 100, 28, 116, 197, 90, 187, 60, 14, 191, 88, 119, 105, 165, 163, 253, 28, 135, 221, 126, 9 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 232, 27, 137, 182, 139, 81, 95, 142, 118, 119, 169, 30, 118, 100, 232, 33, 71, 167, 244, 94 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 16, 23, 91, 33, 117, 47, 185, 143, 161, 170, 158, 50, 157, 236, 19, 83, 199, 136, 142, 181, 3 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 160, 230, 142, 77, 147, 218, 59, 157, 79, 170, 50, 250, 35, 62, 199, 62, 201, 87, 145, 23, 37 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 64, 2, 149, 7, 193, 137, 86, 36, 28, 167, 250, 197, 103, 109, 200, 115, 220, 109, 173, 235, 114, 1 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 128, 22, 210, 75, 138, 97, 97, 107, 25, 135, 202, 187, 13, 70, 212, 133, 156, 74, 198, 52, 125, 14 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 225, 52, 246, 102, 207, 205, 49, 254, 70, 233, 85, 137, 188, 74, 58, 29, 234, 190, 15, 228, 144, 0 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 202, 16, 158, 5, 26, 10, 242, 237, 197, 28, 91, 93, 93, 235, 70, 36, 37, 117, 157, 232, 168, 5 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 228, 167, 44, 56, 4, 101, 116, 75, 187, 31, 143, 165, 165, 49, 197, 106, 115, 147, 38, 22, 153, 56 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 232, 142, 190, 49, 42, 242, 139, 242, 80, 61, 151, 119, 120, 240, 179, 43, 130, 194, 129, 221, 250, 53, 2 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 149, 113, 241, 165, 117, 119, 121, 41, 101, 232, 171, 180, 100, 7, 181, 21, 153, 17, 167, 204, 27, 22 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 160, 210, 111, 110, 123, 152, 170, 190, 158, 243, 19, 183, 14, 239, 73, 18, 217, 250, 175, 134, 254, 21, 221, 0 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 58, 94, 80, 210, 244, 169, 114, 51, 132, 199, 38, 147, 86, 227, 182, 122, 204, 223, 66, 241, 219, 162, 8 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 70, 174, 35, 55, 144, 163, 122, 2, 42, 203, 131, 191, 97, 225, 36, 203, 252, 189, 156, 108, 151, 92, 86 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 193, 206, 100, 39, 162, 99, 202, 24, 164, 239, 37, 123, 209, 205, 112, 239, 223, 107, 31, 62, 234, 157, 95, 3 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 138, 19, 240, 137, 85, 228, 231, 247, 104, 92, 123, 207, 46, 10, 104, 90, 191, 54, 58, 109, 38, 43, 188, 33 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 195, 96, 99, 87, 235, 14, 175, 25, 156, 209, 26, 212, 101, 16, 136, 121, 35, 70, 68, 128, 175, 89, 81, 1 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 232, 161, 199, 225, 105, 49, 149, 214, 0, 25, 48, 12, 73, 250, 163, 80, 191, 98, 189, 170, 2, 219, 128, 45, 13 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 83, 204, 209, 34, 238, 211, 97, 8, 250, 224, 121, 218, 198, 103, 38, 121, 219, 101, 171, 26, 142, 8, 199, 131, 0 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 160, 62, 251, 49, 92, 77, 71, 210, 83, 196, 201, 194, 136, 196, 13, 128, 187, 146, 250, 177, 10, 141, 85, 198, 37, 5 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 114, 208, 243, 153, 5, 201, 54, 70, 171, 225, 155, 87, 173, 137, 0, 83, 187, 201, 243, 106, 130, 87, 191, 121, 51 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 118, 36, 134, 3, 56, 218, 35, 190, 176, 208, 22, 108, 197, 96, 5, 62, 81, 225, 133, 45, 24, 107, 121, 193, 2, 2 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 161, 108, 61, 35, 48, 134, 102, 109, 231, 38, 228, 56, 182, 199, 53, 108, 44, 205, 58, 199, 241, 46, 190, 142, 27, 20 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 74, 62, 102, 96, 225, 61, 1, 70, 10, 133, 233, 56, 30, 205, 25, 58, 188, 3, 76, 200, 113, 213, 109, 147, 19, 201, 0 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 228, 110, 254, 195, 205, 106, 12, 188, 102, 50, 31, 57, 46, 3, 2, 69, 90, 37, 248, 210, 113, 86, 74, 194, 195, 218, 7 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 232, 84, 240, 167, 9, 44, 124, 88, 3, 248, 55, 59, 206, 31, 20, 178, 134, 117, 177, 61, 114, 96, 231, 150, 165, 139, 78 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 81, 99, 143, 96, 184, 217, 116, 33, 176, 47, 80, 14, 62, 201, 244, 66, 151, 238, 104, 118, 196, 9, 229, 119, 116, 17, 3 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 160, 42, 225, 153, 197, 51, 129, 144, 78, 225, 220, 33, 143, 108, 220, 143, 157, 232, 81, 25, 160, 172, 97, 242, 174, 140, 174, 30 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 64, 170, 203, 2, 184, 5, 12, 165, 17, 205, 160, 82, 151, 61, 156, 158, 39, 22, 51, 253, 64, 190, 208, 119, 213, 126, 209, 50, 1 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 128, 166, 244, 27, 48, 57, 120, 114, 176, 2, 72, 58, 233, 103, 26, 50, 140, 221, 254, 227, 137, 110, 39, 174, 86, 244, 46, 252, 11 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 129, 142, 23, 225, 59, 178, 120, 228, 26, 208, 70, 28, 15, 8, 245, 121, 167, 244, 231, 98, 81, 138, 205, 98, 139, 213, 217, 119 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 145, 235, 202, 86, 246, 182, 236, 12, 33, 196, 26, 151, 80, 146, 195, 138, 142, 15, 221, 45, 103, 7, 220, 113, 87, 130, 174, 4 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 100, 170, 51, 237, 99, 159, 37, 63, 129, 74, 169, 11, 231, 37, 183, 163, 107, 145, 155, 162, 202, 7, 74, 152, 114, 106, 23, 209, 46 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 232, 167, 4, 68, 231, 57, 120, 119, 12, 233, 156, 116, 6, 123, 39, 101, 52, 174, 19, 90, 234, 77, 228, 242, 121, 40, 234, 42, 212, 1 }),
            new(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 143, 46, 168, 8, 67, 178, 170, 124, 26, 33, 142, 64, 206, 138, 243, 11, 206, 196, 132, 39, 11, 235, 124, 195, 148, 37, 173, 73, 18 })
        };
    }
}