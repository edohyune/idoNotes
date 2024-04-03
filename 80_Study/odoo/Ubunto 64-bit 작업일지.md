```JavaScript
git clone https://github.com/odoo/odoo.git odoo-dev 로 설치 진행

odoo-dev가 아니라 /opt/odoo로 변경하려고 mv를 이용해 이동하여 진행하려 했으나 여의치 않아 삭제후 

git clone https://github.com/odoo/odoo.git /opt/odoo 로 다시 다운받음
```

  

  

  

  

  

  

  

```JavaScript
git clone https://github.com/odoo/odoo.git /opt/odoo
디렉토리가 없어 생성에 권한 문제 발생
sudo git clone https://github.com/odoo/odoo.git /opt/odoo
로 변경하여 생성함
```