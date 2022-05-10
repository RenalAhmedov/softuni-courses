CREATE TABLE [Users]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Username] VARCHAR(30) NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	[ProfilePicture] VARBINARY(MAX),
	[LastLoginTime] TIME,
	[IsDeleted] BIT,
)
INSERT INTO [Users] VALUES
('Renal', 'asd', NULL, NULL, 0),
('asd', 'zxc', NULL, NULL, 0),
('zxc', 'qwe', NULL, NULL, 1),
('qwe', 'asd', NULL, NULL, 0),
('asd', 'zxc', NULL, NULL, 0)