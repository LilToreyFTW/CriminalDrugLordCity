/**
 * Criminal Drug Lord City - Engine Core Architecture
 * (c) 2026 Criminal Games Inc.
 * 
 * This header defines the underlying memory structures and offsets 
 * used by the game's C++ core before being exposed to C# scripting.
 */

#ifndef CRIMINAL_GAME_ARCH_H
#define CRIMINAL_GAME_ARCH_H

struct PlayerLordData {
    int32_t level;
    int32_t respect;
    float cash;
    float health;
    char rank[64];
};

struct MapOffsets {
    uint32_t componentToWorld = 0x190;
    uint32_t owningGameInstance = 0x190;
    uint32_t playerController = 0x30;
    uint32_t playerState = 0x298;
    uint32_t acknowledgedPawn = 0x318;
    uint32_t location = 0x0;
    uint32_t rotation = 0x18;
    uint32_t mesh = 0x2F8;
};

#endif // CRIMINAL_GAME_ARCH_H
