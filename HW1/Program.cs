using System;


class Program
{
    static void Main()
    {
        string ten, masv, lop, email, github;

        Console.Writeline("Nhập họ và tên:");
        ten = Console.ReadLine();

        Console.WriteLine("Mã sinh viên:");
        masv = Console.ReadLine();

        Console.WriteLine("Lớp:");
        lop = Console.ReadLine();

        Console.WriteLine("Email:");
        email = Console.ReadLine();
       

        Console.WriteLine("Link github:");
        github = Console.ReadLine();

        Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", hoTen, maSV, lop, github, email);
    }
}