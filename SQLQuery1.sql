CREATE PROCEDURE [dbo].[UpadateSalaryPayrollemployee_payroll]
(
 @name varchar(100),
 @basicPay float,
 @Emp_id int
)
AS
BEGIN
Update PayRollTable set basicSalary = @basicPay where PayRollTable.name = @name and PayRollTable.Emp_id = @Emp_id
END