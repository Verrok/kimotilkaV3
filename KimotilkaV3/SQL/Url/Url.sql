exec [dbo].drop_table
     @TableName = 'dbo.[Url]'

create table [dbo].[Url]
(
  [ID]             int identity (1,1) not null,
  [Hash]           varchar(256)       not null,
  [Url]            varbinary(max)     not null,
  [CreateDate]     datetime           not null,
  [FollowCount]    int                not null default (0),
  [IsActive]       bit                not null,
  [ExpirationDate] datetime           null,
  constraint [Url.pk] primary key clustered ([ID], [Hash], [IsActive])
)

create index [Url.ixHashActive]
  on [dbo].[Url] ([Hash], [IsActive])
go