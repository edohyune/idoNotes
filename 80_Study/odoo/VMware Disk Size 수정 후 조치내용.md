아래의 경고 발생

> GPT PMBR size mismatch (41943039 != 146800639) will be corrected by write

조치

```Bash
sudo fdisk /dev/sda
command : p
command : w
```

디바이스에 파티션에 남은공간을 할당

```Bash
>>1. 파티션확장
iadmin@trnsvr:~$ sudo parted /dev/sda
GNU Parted 3.4
Using /dev/sda
Welcome to GNU Parted! Type 'help' to view a list of commands.
(parted) resizepart
Partition number? 3
End?  [21.5GB]? 100%

>>2. LVM Physical Volume (PV) 확장
iadmin@trnsvr:~$ sudo pvresize /dev/sda3
  Physical volume "/dev/sda3" changed
  1 physical volume(s) resized or updated / 0 physical volume(s) not resized

>>3. LVM Logical Volume (LV) 확장
iadmin@trnsvr:~$ sudo lvextend -l +100%FREE /dev/ubuntu-vg/ubuntu-lv
  Size of logical volume ubuntu-vg/ubuntu-lv changed from 18.22 GiB (4665 extents) to 48.22 GiB (12345 extents).
  Logical volume ubuntu-vg/ubuntu-lv successfully resized.

>>4. 파일 시스템 확장
iadmin@trnsvr:~$ sudo resize2fs /dev/ubuntu-vg/ubuntu-lv
resize2fs 1.46.5 (30-Dec-2021)
Filesystem at /dev/ubuntu-vg/ubuntu-lv is mounted on /; on-line resizing required
old_desc_blocks = 3, new_desc_blocks = 7
The filesystem on /dev/ubuntu-vg/ubuntu-lv is now 12641280 (4k) blocks long.
```