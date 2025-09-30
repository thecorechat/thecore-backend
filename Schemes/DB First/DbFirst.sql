use [SchoolChat]

CREATE TABLE chats(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Title nvarchar(256) NOT NULL
);

CREATE TABLE messages(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Chat_Id INT NOT NULL,
	Author nvarchar(256) NOT NULL,
	Message nvarchar(max) NOT NULL,
	Created_At SmallDateTime DEFAULT(getdate()) NOT NULL,

	CONSTRAINT FK_Messages_Char_Id_Id FOREIGN KEY(Chat_Id) REFERENCES chats(Id)
);