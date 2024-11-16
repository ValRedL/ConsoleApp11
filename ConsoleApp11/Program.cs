using System;

class ATM
{
    static void Main()
    {
        decimal balance = 1000m; // ยอดเงินเริ่มต้น
        bool isRunning = true;

        // อัตราภาษีและช่วงรายได้สุทธิ
        decimal[] taxRates = { 0.05m, 0.10m, 0.15m, 0.20m, 0.25m, 0.30m, 0.35m };
        decimal[] taxBrackets = { 150000, 300000, 500000, 750000, 1000000, 2000000, 5000000 };
        decimal[] accumulatedTaxes = { 0, 7500, 27500, 65000, 115000, 365000, 1265000 };

        while (isRunning)
        {
            Console.WriteLine("Welcome to the ATM");
            Console.WriteLine("1. Deposit Money");
            Console.WriteLine("2. Withdraw Money");
            Console.WriteLine("3. Calculate Tax");
            Console.WriteLine("4. Exit");
            Console.Write("Please select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Deposit(ref balance);
                    break;
                case "2":
                    Withdraw(ref balance);
                    break;
                case "3":
                    CalculateTax(taxRates, taxBrackets, accumulatedTaxes);
                    break;
                case "4":
                    Console.WriteLine("Thank you for using the ATM. Goodbye!");
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine($"\nCurrent balance: {balance:C}\n");
        }
    }

    static void Deposit(ref decimal balance)
    {
        Console.Write("Enter amount to deposit: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
        {
            balance += amount;
            Console.WriteLine($"Successfully deposited {amount:C}");
        }
        else
        {
            Console.WriteLine("Invalid amount. Please try again.");
        }
    }

    static void Withdraw(ref decimal balance)
    {
        Console.Write("Enter amount to withdraw: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
        {
            if (amount <= balance)
            {
                balance -= amount;
                Console.WriteLine($"Successfully withdrew {amount:C}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }
        else
        {
            Console.WriteLine("Invalid amount. Please try again.");
        }
    }

    static void CalculateTax(decimal[] taxRates, decimal[] taxBrackets, decimal[] accumulatedTaxes)
    {
        Console.Write("Enter income and bonus: ");
        decimal incomeAndBonus = decimal.Parse(Console.ReadLine());

        Console.Write("Enter personal expenses: ");
        decimal personalExpenses = decimal.Parse(Console.ReadLine());

        Console.Write("Enter personal deductions: ");
        decimal personalDeductions = decimal.Parse(Console.ReadLine());

        decimal grossIncome = incomeAndBonus;
        decimal netIncome = grossIncome - personalExpenses - personalDeductions;

        Console.WriteLine($"\nGross Income: {grossIncome:C}");
        Console.WriteLine($"Net Income after expenses and deductions: {netIncome:C}");

        decimal taxAmount = CalculateTaxAmount(netIncome, taxRates, taxBrackets, accumulatedTaxes);
        Console.WriteLine($"Total Tax Payable: {taxAmount:C}");
    }

    static decimal CalculateTaxAmount(decimal netIncome, decimal[] taxRates, decimal[] taxBrackets, decimal[] accumulatedTaxes)
    {
        for (int i = taxBrackets.Length - 1; i >= 0; i--)
        {
            if (netIncome > taxBrackets[i])
            {
                // ใช้สูตร: [(เงินได้สุทธิ - รายได้สูงสุดของขั้นก่อนหน้า) * อัตราภาษี] + ภาษีสะสมของขั้นก่อนหน้า
                return (netIncome - taxBrackets[i]) * taxRates[i] + accumulatedTaxes[i];
            }
        }

        // ถ้ารายได้น้อยกว่า 150,000 บาท (ยกเว้นภาษี)
        return 0;
    }
}
