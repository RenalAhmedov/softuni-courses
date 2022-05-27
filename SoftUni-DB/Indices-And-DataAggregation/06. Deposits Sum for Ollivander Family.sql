SELECT DepositGroup,
       SUM(DepositAmount) as TotalSum
  FROM WizzardDeposits
 WHERE MagicWandCreator = 'Ollivander Family'
 GROUP BY DepositGroup
