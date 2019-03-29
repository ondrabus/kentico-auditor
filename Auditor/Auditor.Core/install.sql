IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Auditor_Log'))
BEGIN
	CREATE TABLE [dbo].[Auditor_Log](
	[LogGuid] [uniqueidentifier] NOT NULL,
	[SiteGuid] [uniqueidentifier] NULL,
	[UserGuid] [uniqueidentifier] NOT NULL,
	[ObjectGuid] [uniqueidentifier] NOT NULL,
	[SecondObjectGuid] [uniqueidentifier] NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[Action] [nvarchar](100) NOT NULL,
	[ObjectName] [nvarchar](100) NULL,
	[SecondObjectName] [nvarchar](100) NULL,
	[Data] [nvarchar](max) NULL,
	CONSTRAINT [PK_Auditor_Log] PRIMARY KEY CLUSTERED 
	(
		[LogGuid] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

	ALTER TABLE [dbo].[Auditor_Log] ADD  CONSTRAINT [DF_LogId]  DEFAULT (newsequentialid()) FOR [LogGuid];
END