set nocount on
set ansi_nulls, ansi_warnings, arithabort, ansi_padding on
set numeric_roundabort off
set transaction isolation level read uncommitted
set xact_abort on
go



create procedure dbo.create_proc
    @ObjectName varchar(max)
as
begin
    declare
        @objid int
        , @sql nvarchar(4000) = 'create procedure ' + @ObjectName + ' as begin return(-1) end'
    -------------------------------------------
    select @objid = object_id(@ObjectName)
    -------------------------------------------
    if (@objid is not null)
        begin
            return (0)
        end


    exec (@sql)

end

