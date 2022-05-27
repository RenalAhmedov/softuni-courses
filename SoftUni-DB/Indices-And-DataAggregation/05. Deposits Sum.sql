SELECT DepositGroup,
       SUM(DepositAmount) as TotalSum
  FROM WizzardDeposits
 GROUP BY DepositGroup
