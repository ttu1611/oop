using System;

class Person
{
    public string HoTen;
    public string DiaChi;
    public double Luong;
}

class Program
{
    public static Person NhapThongTinPerson(string hoTen, string diaChi, string sLuong)
    {
        if (string.IsNullOrWhiteSpace(sLuong))
        {
            throw new Exception("Bạn phải nhập Lương.");
        }

        double luong;
        if (!double.TryParse(sLuong, out luong))
        {
            throw new Exception("Bạn phải nhập chữ số.");
        }

        if (luong <= 0)
        {
            throw new Exception("Lương phải lớn hơn 0");
        }

        Person p = new Person();
        p.HoTen = hoTen;
        p.DiaChi = diaChi;
        p.Luong = luong;
        return p;
    }

    public static void HienThiThongTinPerson(Person p)
    {
        Console.WriteLine($"Họ tên: {p.HoTen}, Địa chỉ: {p.DiaChi}, Lương: {p.Luong}");
    }

    public static Person[] SapXepTheoLuong(Person[] ds)
    {
        if (ds == null || ds.Length == 0)
        {
            throw new Exception("Không thể sắp xếp Person");
        }

        for (int i = 0; i < ds.Length - 1; i++)
        {
            for (int j = i + 1; j < ds.Length; j++)
            {
                if (ds[i].Luong > ds[j].Luong)
                {
                    Person tam = ds[i];
                    ds[i] = ds[j];
                    ds[j] = tam;
                }
            }
        }
        return ds;
    }

    static void Main(string[] args)
    {
        try
        {
            Console.Write("Nhập số lượng người: ");
            int n = int.Parse(Console.ReadLine());

            Person[] ds = new Person[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Người thứ {i + 1}:");
                Console.Write("  Họ tên: ");
                string hoTen = Console.ReadLine();
                Console.Write("  Địa chỉ: ");
                string diaChi = Console.ReadLine();
                Console.Write("  Lương: ");
                string sLuong = Console.ReadLine();

                ds[i] = NhapThongTinPerson(hoTen, diaChi, sLuong);
            }

            Console.WriteLine("\n--- Danh sách vừa nhập ---");
            foreach (Person p in ds)
            {
                HienThiThongTinPerson(p);
            }

            SapXepTheoLuong(ds);
            Console.WriteLine("\n--- Danh sách sau khi sắp xếp theo lương tăng dần ---");
            foreach (Person p in ds)
            {
                HienThiThongTinPerson(p);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Lỗi: " + e.Message);
        }
    }
}
