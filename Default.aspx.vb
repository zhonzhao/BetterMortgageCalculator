
'Be sure to import system.data so you can create data tables on the fly.
Imports System.Data
Partial Class _Default
    Inherits System.Web.UI.Page
    'Adapted from the loan calculator found at 
    'www.dreamincode.net/forums/topic/237228-looping-issues-using-a-grid-for-mortgage-calculator-amortization/
    Protected Sub btnCalcPmt_Click(sender As Object, e As EventArgs) Handles btnCalcPmt.Click
        'Declaring the Variables for each field.
        Dim loanAmount As Double
        Dim annualRate As Double
        Dim interestRate As Double
        Dim term As Integer
        Dim loanTerm As Integer
        Dim monthlyPayment As Double
        'This section is Declaring the Variables for Loan Amortization.
        Dim interestPaid As Double
        Dim nBalance As Double
        Dim principal As Double
        'Declaring a table to hold the payment information.
        Dim table As DataTable = New DataTable("ParentTable")
        Dim loanAmortTbl As DataTable = New DataTable("AmortizationTable")
        Dim tRow As DataRow
        'This section adds default values to the Variables.
        interestPaid = 0.0
        'This Section Converts each input string to the appropriate variable assigned.
        loanAmount = CDbl(tbLoanAmt.Text)
        annualRate = CDbl(tbAnnualInterest.Text)
        term = CDbl(tbLoanTerm.Text)
        'This Section Formats the Loan Input to Currency.
        tbLoanAmt.Text = FormatCurrency(loanAmount)
        'Converting the Annual Rate to a Monthly Rate by dividing 5.75% by 100 * 12 (months in a year) gives you 0.00479.
        interestRate = annualRate / (100 * 12)
        'Converting the Years (Term) into Months (Loan Term) by Multipling the Years by 12.
        loanTerm = term * 12
        'Calculating the Monthly Payment using the Converted Interest Rate and Loan Term.
        monthlyPayment = loanAmount * interestRate / (1 - Math.Pow((1 + interestRate), (-loanTerm)))
        'Displaying the Monthly Payment in the TextBox and converts the Variable to Currency.
        lblMonthlyPmt.Text = FormatCurrency(monthlyPayment)
        'Adds Items to List Box, Formats them for Currency and Adds Pad Spacing for each item.
        loanAmortTbl.Columns.Add("Payment Number", System.Type.GetType("System.String"))
        loanAmortTbl.Columns.Add("Principal Paid", System.Type.GetType("System.String"))
        loanAmortTbl.Columns.Add("Interest Paid", System.Type.GetType("System.String"))
        loanAmortTbl.Columns.Add("New Balance", System.Type.GetType("System.String"))
        'This section uses the for loop to display the Loan Balance and Interest Paid over the Term of the Loan.
        Dim counterStart As Integer
        For counterStart = 1 To loanTerm
            'Performs Calculations for Amortization of loan.
            interestPaid = loanAmount * interestRate
            principal = monthlyPayment - interestPaid
            nBalance = loanAmount - principal
            loanAmount = nBalance
            'Writes the data to a new row in the gridview.
            tRow = loanAmortTbl.NewRow()
            tRow("Payment Number") = String.Format(counterStart)
            tRow("Principal Paid") = String.Format("{0:C}", principal)
            ' String.Format("{0:C},principal) formats the variable "prinicpal" as currency(C).
             tRow("Interest Paid") = String.Format("{0:C}", interestPaid)
            tRow("New Balance") = String.Format("{0:C}", nBalance)
            loanAmortTbl.Rows.Add(tRow)
            'Loops to next counterStart (Continues loop to requirements are met).
        Next counterStart
        loanGridView.DataSource = loanAmortTbl
        loanGridView.DataBind()
    End Sub
End Class

