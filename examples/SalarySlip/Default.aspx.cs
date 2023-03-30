using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

// Author: Mitul Vaghamshi
namespace SalarySlip
{
	public partial class _Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			const string labelStyle = "col-md-6 control label align-self-center";
			const string inputStyle = "col-md-5 control";

			// Gross Salary
			LabelSalary.Text = "What is your gross salary:";
			LabelSalary.CssClass = labelStyle;
			// Input Salary
			InputSalary.TextMode = TextBoxMode.Number;
			InputSalary.CssClass = inputStyle;
			InputSalary.TabIndex = 1;
			
			// Label Bonus
			LabelBonus.Text = "Enter bonus received:";
			LabelBonus.CssClass = labelStyle;
			// Input Bonus
			InputBonus.TextMode = TextBoxMode.Number;
			InputBonus.CssClass = inputStyle;
			InputBonus.TabIndex = 2;

			// Label Allowance
			LabelAllowance.Text = "How much allowance:";
			LabelAllowance.CssClass = labelStyle;
			// Input Allowance
			InputAllowance.TextMode = TextBoxMode.Number;
			InputAllowance.CssClass = inputStyle;
			InputAllowance.TabIndex = 3;

			// Label Tax
			LabelTax.Text = "Total tax (fed + prov):";
			LabelTax.CssClass = labelStyle;
			// Input Tax
			InputTax.CssClass = inputStyle;
			InputTax.ReadOnly = true;

			// Label CPP
			LabelCpp.Text = "CPP deductions:";
			LabelCpp.CssClass = labelStyle;
			// Input CPP
			InputCpp.CssClass = inputStyle;
			InputCpp.ReadOnly = true;

			// Label EI
			LabelEi.Text = "EI deductions:";
			LabelEi.CssClass = labelStyle;
			// Input Ei
			InputEi.CssClass = inputStyle;
			InputEi.ReadOnly = true;

			// Label SalaryAfter Tax
			LabelSalaryAfterTax.Text = "Salary after tax:";
			LabelSalaryAfterTax.CssClass = labelStyle;
			// Input SalaryAfter Tax
			InputSalaryAfterTax.CssClass = inputStyle;
			InputSalaryAfterTax.ReadOnly = true;

			// Label Gender
			LabelGender.Text = "Select your gender:";
			LabelGender.CssClass = labelStyle;
			// DropDownBox Gender
			DropDownListGender.CssClass = inputStyle;
			DropDownListGender.TabIndex = 4;

			// Label Dependants
			LabelDependants.Text = "Number of dependants:";
			LabelDependants.CssClass = labelStyle;
			// DropDownList Dependants
			DropDownListDependants.CssClass = inputStyle;
			DropDownListDependants.TabIndex = 5;

			// Label Total
			LabelTotal.Text = "Your net salary + Benefits:";
			LabelTotal.CssClass = labelStyle;
			// Input Total
			InputTotal.CssClass = inputStyle;
			InputTotal.ReadOnly = true;

			// Button Calculate
			InputCalculate.Text = "Calculate";
			InputCalculate.CssClass = "control calc-btn";
			InputCalculate.TabIndex = 6;
		}

		protected void InputCalculate_Click(object sender, EventArgs e)
		{
			double allowance = Validate(InputAllowance);
			double bonus = Validate(InputBonus);
			double grossSalary = Validate(InputSalary);

			if (grossSalary < 0 || bonus < 0 || allowance < 0) return;

			var result = Calculate(grossSalary + bonus + allowance);
			InputTax.Text = $"${result["totalTax"]:n2}";
			InputCpp.Text = $"${result["cppDeductions"]:n2}";
			InputEi.Text = $"${result["eiDeductions"]:n2}";
			InputSalaryAfterTax.Text = $"${result["afterTaxIncome"]:n2}";

			var finalSalary = result["afterTaxIncome"];
			finalSalary *= (1 + double.Parse(DropDownListGender.SelectedValue) / 100);
			finalSalary *= (1 + double.Parse(DropDownListDependants.SelectedValue) / 100);
			InputTotal.Text = $"{finalSalary:n2}";
		}

		private double Validate(TextBox element)
		{
			try
			{
				var value = float.Parse(element.Text);
				if (!double.IsNaN(value) && value >= 0) return value;
			}
			catch
			{
				element.Focus();
			}
			return -1;
		}

		private Dictionary<string, double> Calculate(double salary)
		{
			double ei_premiums = Math.Max(0, Math.Min(salary, 61500)) * 0.0158;
			double cpp_premiums = (Math.Max(0, Math.Min(salary, 66600) - 3500) * 0.163) / 2;
			double cpp_deduction = Math.Max(0, Math.Min(salary, 66600) - 3500) * 0.0075;
			double payroll_deductions = cpp_premiums + ei_premiums;
			double payroll_tax_credits = cpp_premiums - cpp_deduction + ei_premiums;
			double income = salary + cpp_deduction;

			double f_tax;
			// Federal Tax
			if (income <= 50197)
			{
				f_tax = income * 0.15;
			}
			else if (income <= 100392)
			{
				f_tax = (income - 50197) * 0.205 + 7529.55;
			}
			else if (income <= 155625)
			{
				f_tax = (income - 100392) * 0.26 + 17819.53;
			}
			else if (income <= 221708)
			{
				f_tax = (income - 155625) * 0.29 + 32180.11;
			}
			else
			{
				f_tax = (income - 221708) * 0.33 + 51344.18;
			}

			double fed_bpa = 12719;
			if (income < 155625)
			{
				fed_bpa += 1679;
			}
			else if (income < 221708)
			{
				fed_bpa += 1679 - (income - 155625) * 0.025407442;
			}
			f_tax = Math.Max(f_tax - (fed_bpa + Math.Min(1287, salary) + payroll_tax_credits) * 0.15, 0);

			double p_tax;
			// Provincial Tax
			if (income <= 46226)
			{
				p_tax = income * 0.0505;
			}
			else if (income <= 92454)
			{
				p_tax = (income - 46226) * 0.0915 + 2334.41;
			}
			else if (income <= 150000)
			{
				p_tax = (income - 92454) * 0.1116 + 6564.28;
			}
			else if (income <= 220000)
			{
				p_tax = (income - 150000) * 0.1216 + 12986.41;
			}
			else
			{
				p_tax = (income - 220000) * 0.1316 + 21498.41;
			}
			p_tax = Math.Max(p_tax - (11141 + payroll_tax_credits) * 0.0505, 0);

			double s_tax;
			/* ON Surtax */
			if (p_tax >= 6387)
			{
				s_tax = (p_tax - 4991) * 0.2 + (p_tax - 6387) * 0.36;
			}
			else if (p_tax >= 4991)
			{
				s_tax = (p_tax - 4991) * 0.2;
			}
			else
			{
				s_tax = 0;
			}

			/* ON DTC After Surtax */
			p_tax += s_tax;

			// Ontario Health Premium
			double on_health = 0;
			if (income > 200600)
			{
				on_health = 900;
			}
			else if (income > 200000)
			{
				on_health = (income - 200000) * 0.25 + 750;
			}
			else if (income > 72600)
			{
				on_health = 750;
			}
			else if (income > 72000)
			{
				on_health = (income - 72000) * 0.25 + 600;
			}
			else if (income > 48600)
			{
				on_health = 600;
			}
			else if (income > 48000)
			{
				on_health = (income - 48000) * 0.25 + 450;
			}
			else if (income > 38500)
			{
				on_health = 450;
			}
			else if (income > 36000)
			{
				on_health = (income - 36000) * 0.06 + 300;
			}
			else if (income > 25000)
			{
				on_health = 300;
			}
			else if (income > 20000)
			{
				on_health = (income - 20000) * 0.06 + 0;
			}
			p_tax += on_health;

			double totalTax = f_tax + p_tax + payroll_deductions + 0;
			double afterTaxIncome = salary - totalTax;

			return new Dictionary<string, double>
			{
				{ "totalTax", totalTax },
				{ "cppDeductions", payroll_deductions },
				{ "eiDeductions", ei_premiums },
				{ "afterTaxIncome", afterTaxIncome }
			};
		}
	}
}