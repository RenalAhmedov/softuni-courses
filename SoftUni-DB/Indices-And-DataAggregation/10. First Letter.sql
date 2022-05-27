SELECT SUBSTRING([FirstName], 1,1) AS [FirstLetter]
	FROM [WizzardDeposits]
	GROUP BY SUBSTRING([FirstName], 1,1), [DepositGroup]
	HAVING [DepositGroup] = 'Troll Chest' 