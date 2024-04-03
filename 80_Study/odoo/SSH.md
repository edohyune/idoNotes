Ubuntu 처음 설치 시 옵션으로 설치 가능

### Client

Client SSH Key 생성

```Bash
ssh-keygen -t rsa -b 4096
```

**C:\Users\[YOUR_USERNAME]\.ssh 에 저장**

### Server

### Uninstall

openssh-server

```Bash
sudo apt-get remove --purge openssh-server
```

openssh-client

```Bash
sudo apt-get remove --purge openssh-client
```

### Install

```Bash
>>update 확인
sudo apt update
>>설치
sudo apt install openssh-server
>>설치 확인
sudo systemctl status ssh
 - active (running)
>>포트열기 
sudo ufw allow ssh
>>클라이언트 설치
sudo apt install openssh-client
```