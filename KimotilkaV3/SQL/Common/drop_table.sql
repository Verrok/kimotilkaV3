set nocount on
set ansi_nulls, ansi_warnings, arithabort, ansi_padding on
set numeric_roundabort off
set transaction isolation level read uncommitted
set xact_abort on
go

--proc name--
exec dbo.create_proc
     @ObjectName = 'dbo.drop_table'
go

alter procedure dbo.drop_table
    --vars--
    @TableName sysname
    --------
    as
    begin
        set nocount on
        set ansi_nulls, ansi_warnings, arithabort, ansi_padding on
        set numeric_roundabort off
        set transaction isolation level read uncommitted
        set xact_abort on


        exec('declare @count int, @err int = null
      if OBJECT_ID(''' + @TableName + ''') is not null begin select @count = count(*) from ' + @TableName + ' end
      if @count = 0 and @err is null
      begin
      exec dbo.drop_fks
                 @TableName = ''' + @TableName + ''' drop table if exists ' + @TableName + ' end')


    end


