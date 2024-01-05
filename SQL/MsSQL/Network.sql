USE [tetest]
GO

/****** Object:  Table [dbo].[Network]    Script Date: 1/5/2024 8:38:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Network](
	[computer] [varchar](100) NOT NULL,
	[status1] [varchar](20) NULL,
	[station] [varchar](100) NULL,
	[type_mode] [varchar](100) NULL,
	[model] [varchar](100) NULL,
	[performency] [varchar](50) NULL,
	[test_times] [int] NULL,
	[pass_times] [int] NULL,
	[date] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[computer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Network]  WITH CHECK ADD  CONSTRAINT [check_status1] CHECK  (([status1]='connected' OR [status1]='disconnected'))
GO

ALTER TABLE [dbo].[Network] CHECK CONSTRAINT [check_status1]
GO

