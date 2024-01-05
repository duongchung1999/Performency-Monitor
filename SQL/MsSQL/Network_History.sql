USE [tetest]
GO

/****** Object:  Table [dbo].[Network_History]    Script Date: 1/5/2024 8:36:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Network_History](
	[computer] [varchar](250) NOT NULL,
	[status1] [varchar](100) NULL,
	[time_change] [datetime] NULL,
	[model] [varchar](50) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Network_History]  WITH CHECK ADD  CONSTRAINT [connect_check3] CHECK  (([status1]='connected' OR [status1]='disconnected'))
GO

ALTER TABLE [dbo].[Network_History] CHECK CONSTRAINT [connect_check3]
GO

