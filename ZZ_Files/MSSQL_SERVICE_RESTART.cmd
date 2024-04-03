@echo off
echo.

net stop SQLSERVERAgent
net stop mssqlserver


net start mssqlserver
net start SQLSERVERAgent
