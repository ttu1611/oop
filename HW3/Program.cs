using System;
using System.Collections.Generic;

class Employee
{
    public string Name { get; set; }

    public Employee(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}

class Item
{
    public string Name { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }  

    public Item(string name, double price, double discount = 0.0)
    {
        Name = name;
        Price = price;
        Discount = discount;
    }

    public double GetPrice() => Price;
    public double GetDiscount() => Discount;

    public override string ToString()
        => $"{Name} - ${Price:0.00} (Discount: ${Discount:0.00})";
}

class BillLine
{
    public Item Item { get; private set; }
    public int Quantity { get; private set; }

    public void SetItem(Item item)
    {
        Item = item;
    }

    public Item GetItem() => Item;

    public void SetQuantity(int quantity)
    {
        Quantity = quantity;
    }

    public int GetQuantity() => Quantity;

    public double GetLineTotal()
    {
        return Item.GetPrice() * Quantity;
    }

    public double GetLineDiscount()
    {
        return Item.GetDiscount() * Quantity;
    }

    public override string ToString()
        => $"{Quantity} x {Item.Name} @ ${Item.Price:0.00} each (Discount ${Item.Discount:0.00} each)";
}

class GroceryBill
{
    protected Employee clerk;
    protected List<BillLine> billLines = new List<BillLine>();

    public GroceryBill(Employee clerk)
    {
        this.clerk = clerk;
    }

    public void Add(BillLine bl)
    {
        billLines.Add(bl);
    }

    public virtual double GetTotal()
    {
        double sum = 0;
        foreach (var bl in billLines)
        {
            sum += bl.GetLineTotal();
        }
        return sum;
    }

    public virtual void PrintReceipt()
    {
        Console.WriteLine($"Clerk: {clerk}");
        Console.WriteLine("=== RECEIPT ===");
        foreach (var bl in billLines)
        {
            Console.WriteLine(bl);
        }
        Console.WriteLine($"TOTAL: ${GetTotal():0.00}");
    }
}

class DiscountBill : GroceryBill
{
    private bool preferred;

    public DiscountBill(Employee clerk, bool preferred)
        : base(clerk)
    {
        this.preferred = preferred;
    }

    public int GetDiscountCount()
    {
        if (!preferred) return 0;
        int count = 0;
        foreach (var bl in billLines)
        {
            if (bl.Item.GetDiscount() > 0)
                count += bl.Quantity;
        }
        return count;
    }

    public double GetDiscountAmount()
    {
        if (!preferred) return 0.0;
        double totalDiscount = 0;
        foreach (var bl in billLines)
        {
            totalDiscount += bl.GetLineDiscount();
        }
        return totalDiscount;
    }

    public double GetDiscountPercent()
    {
        if (!preferred) return 0.0;
        double before = base.GetTotal();
        double discount = GetDiscountAmount();
        if (before == 0) return 0;
        return (discount / before) * 100;
    }

    public override double GetTotal()
    {
        double total = base.GetTotal();
        if (preferred)
            total -= GetDiscountAmount();
        return total;
    }

    public override void PrintReceipt()
    {
        base.PrintReceipt();
        if (preferred)
        {
            Console.WriteLine($"Discounted items: {GetDiscountCount()}");
            Console.WriteLine($"Total discount: ${GetDiscountAmount():0.00}");
            Console.WriteLine($"Discount percent: {GetDiscountPercent():0.0}%");
        }
        else
        {
            Console.WriteLine("(No discounts applied)");
        }
    }
}

class Program
{
    static void Main()
    {
        Item candy = new Item("Candy Bar", 1.35, 0.25);
        Item milk = new Item("Milk", 2.50);
        Item chips = new Item("Potato Chips", 3.00, 0.50);

        BillLine bl1 = new BillLine();
        bl1.SetItem(candy);
        bl1.SetQuantity(4);

        BillLine bl2 = new BillLine();
        bl2.SetItem(milk);
        bl2.SetQuantity(2);

        BillLine bl3 = new BillLine();
        bl3.SetItem(chips);
        bl3.SetQuantity(3);

        Employee clerk = new Employee("Alex");

        Console.WriteLine("\n=== Normal Customer ===");
        GroceryBill normalBill = new GroceryBill(clerk);
        normalBill.Add(bl1);
        normalBill.Add(bl2);
        normalBill.Add(bl3);
        normalBill.PrintReceipt();

        Console.WriteLine("\n=== Preferred Customer ===");
        DiscountBill preferredBill = new DiscountBill(clerk, true);
        preferredBill.Add(bl1);
        preferredBill.Add(bl2);
        preferredBill.Add(bl3);
        preferredBill.PrintReceipt();
    }
}