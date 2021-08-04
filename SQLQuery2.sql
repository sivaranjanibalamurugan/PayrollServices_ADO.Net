CREATE PROCEDURE [dbo].[RetriveData]
(
@startDate date,
@endDate date
)
AS
BEGIN
select * from employee_payroll where startDate between cast('2020-07-31' as date) and getdate();
END