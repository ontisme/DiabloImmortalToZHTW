import time
from typing import List
import pymem

game_name = "DiabloImmortal.exe"
pm = pymem.Pymem(game_name)
offset = [0x03D37000, 0x03F5E230, 0x03D8DA30]
pointer = [[0x1A8], [0x1D8], [0xE8]]


def resolve_address(addr_offsets: List[int], base: int) -> int:
    # Credit to https://github.com/amacati/SoulsGym/blob/4d64695708953860e376f74cb04c095d762d5307/soulsgym/core/memory_manipulator.py
    helper = pm.read_longlong(base)
    for o in addr_offsets[:-1]:
        helper = pm.read_longlong(helper + o)
    helper += addr_offsets[-1]
    return helper


try:
    hwnd = pymem.process.module_from_name(pm.process_handle, game_name).lpBaseOfDll
    for i in range(len(offset)):
        m_locate = resolve_address(pointer[i], hwnd + offset[i])
        a = pm.write_string(m_locate, "zhTW")
        print(f"已修改記憶體 {hex(m_locate)} 為 zhTW")
except Exception as e:
    print(e)
    print("可能沒有啟動遊戲")
finally:
    print("程序結束，請手動至遊戲內修改韓文語系，如失敗可嘗試重啟幾次遊戲反覆操作")
    print("若多次失敗，且手動能力高者，可自行搜尋 Array of Bytes [6B 6F 4B 52 00 00 00 00 ?? 00 00 00 00 00 00 00] 修改")
    print("此程式由 看門狗[watchdog.fun] 站長製作")
    time.sleep(5)
