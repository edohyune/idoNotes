trnsvr  
iadmin/iadmin  

  

> [!info] How To Install Odoo 16 on Ubuntu 22.04  
> How To Install Odoo 16 on Ubuntu 22.  
> [https://www.youtube.com/watch?v=VblqEEFY7Cs](https://www.youtube.com/watch?v=VblqEEFY7Cs)  

  

> ####################################  
> Operating System: Ubuntu22.04 LTS  
> IP Address : 10.66.10.8  
> RAM : 2GB  
> Disk : 50GB  
> Service : Odoo  
> Host Name : odoo.technologyrss.local  
> ####################################  

### Step \#01: Must be server update and upgrade then run some dependency package.

```Bash
root@odoo:~# lsb_release -a && ip r
root@odoo:~# apt update && sudo apt upgrade
root@odoo:~# sudo apt install -y git wget nodejs npm python3 build-essential libzip-dev python3-dev libxslt1-dev python3-pip libldap2-dev python3-wheel libsasl2-dev python3-venv python3-setuptools node-less libjpeg-dev xfonts-75dpi xfonts-base libpq-dev libffi-dev fontconfig
```

Install pdf generator package.

```Bash
root@odoo:~# sudo npm install -g rtlcss
root@odoo:~# wget https://github.com/wkhtmltopdf/packaging/releases/download/0.12.6.1-2/wkhtmltox_0.12.6.1-2.jammy_amd64.deb
root@odoo:~# sudo dpkg -i wkhtmltox_0.12.6.1-2.jammy_amd64.deb
```

### Step \#02: Add user and install database postgresql.

```Bash
root@odoo:~# sudo adduser --system --group --home=/opt/odoo --shell=/bin/bash odoo
root@odoo:~# sudo apt install postgresql -y
root@odoo:~# service postgresql start
root@odoo:~# service postgresql status
root@odoo:~# sudo su - postgres -c "createuser -s odoo"
root@odoo:~# cd /opt/odoo
```

Download odoo latest branch from below link.

```Bash
root@odoo:/opt/odoo# git clone https://github.com/odoo/odoo.git --depth 1 --branch 16.0 --single-branch odoo-server
```

Setup permission and going to server location folder.

```Bash
root@odoo:~# sudo chown -R odoo:odoo /opt/odoo/odoo-server
root@odoo:~# cd /opt/odoo/odoo-server
```

Active venv terminal

```Plain
root@odoo:/opt/odoo/odoo-server# python3 -m venv venv
root@odoo:/opt/odoo/odoo-server# source venv/bin/activate
(venv) root@odoo:/opt/odoo/odoo-server# pip3 install wheel
```

### Step \#03: Install requirements.txt file.

```Bash
(venv) root@odoo:/opt/odoo-odoo-server# pip3 install -r requirements.txt
```

Then exit venv terminal using below command.

```Bash
(venv) root@odoo:/opt/odoo/odoo-server# deactivate
```

Setup odoo user permission.

```Bash
root@odoo:~# sudo mkdir /var/log/odoo
root@odoo:~# sudo chown odoo:odoo /var/log/odoo
root@odoo:~# sudo chmod 777 /var/log/odoo
```

### Step \#04: Create odoo server conf file

```Bash
root@odoo:~# sudo nano /etc/odoo-server.conf
```

Then insert below all lines into this file. This file contain odoo master password its needed when create database from browser.

```Plain
[options]
admin_passwd = P@ss$123
db_user = odoo
addons_path = /opt/odoo/odoo-server/addons
logfile = /var/log/odoo/odoo-server.log
log_level  = debug
```

Setup file user permission.

```Bash
sudo chown odoo:odoo /etc/odoo-server.conf
```

Create odoo service file.

```Bash
root@odoo:~# sudo nano /etc/systemd/system/odoo.service
```

Then insert below all lines into this file.

```Plain
[Unit]
Description=Odoo 16.0 Service
Requires=postgresql.service
After=network.target postgresql.service

[Service]
Type=simple
SyslogIdentifier=odoo
PermissionsStartOnly=true
User=odoo
Group=odoo
ExecStart=/opt/odoo/odoo-server/venv/bin/python3 /opt/odoo/odoo-server/odoo-bin -c /etc/odoo-server.conf
StandardOutput=journal+console

[Install]
WantedBy=multi-user.target
```

Now reload deamon.

```Plain
root@odoo:~# sudo systemctl daemon-reload
root@odoo:~# sudo systemctl enable --now odoo.service
root@odoo:~# sudo systemctl status odoo.service
```

  

Now going to your server ip with port then create database and import demo data.