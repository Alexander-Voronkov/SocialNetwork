#!/bin/bash
echo "Starting entrypoint.sh"

/opt/mssql/bin/sqlservr &

echo "Waiting for SQL Server to start..."
while ! pgrep -x "sqlservr" > /dev/null; do
    sleep 1
done
echo "SQL Server is now running."

echo "Waiting for SQL Server to be ready for connections..."
while ! /opt/mssql-tools/bin/sqlcmd -S localhost,$DB_PORT -U $DB_USERID -P $DB_PASS -Q "SELECT 1"; do
    sleep 1
done
echo "SQL Server is ready for connections."

echo "Executing sql"
/opt/mssql-tools/bin/sqlcmd -S localhost,$DB_PORT -U $DB_USERID -P $DB_PASS -i /usr/config/setup.sql

echo "End entrypoint.sh"

while true; do
    sleep 1
done