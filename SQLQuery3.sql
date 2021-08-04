CREATE PROCEDURE [dbo].[Aggregate]
(
@NetPay int,
@gender varchar(1)
)
AS
BEGIN
select sum(PayRoll.NetPay) as totalsalary,Employee.gender from PayRoll,Employee group by gender
select max(PayRoll.NetPay) as Minsalary,Employee.gender from PayRoll,Employee group by gender
select min(PayRoll.NetPay) as Maxsalary,Employee.gender from PayRoll,Employee group by gender
select avg(PayRoll.NetPay) as Avgsalary,Employee.gender from PayRoll,Employee group by gender
END