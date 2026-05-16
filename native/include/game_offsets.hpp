#pragma once

#include <cstdint>

namespace cdlc
{
    struct Vec3
    {
        float x;
        float y;
        float z;
    };

    struct MapOffsets
    {
        const char* mapId;
        std::uint32_t terrainWidth;
        std::uint32_t terrainHeight;
        std::uint32_t terrainHeightFieldOffset;
        std::uint32_t roadNetworkOffset;
        std::uint32_t arsenalRoomOffset;
        std::uint32_t vehicleSpawnOffset;
    };

    struct AssetStruct
    {
        const char* assetId;
        const char* assetType;
        std::uint32_t vertexOffset;
        std::uint32_t indexOffset;
        std::uint32_t materialOffset;
        Vec3 pivot;
    };

    struct RuntimeOffsetTable
    {
        std::uint32_t componentToWorld;
        std::uint32_t owningGameInstance;
        std::uint32_t persistentLevel;
        std::uint32_t localPlayers;
        std::uint32_t playerController;
        std::uint32_t playerState;
        std::uint32_t playerNamePrivate;
        std::uint32_t owningActor;
        std::uint32_t maxPacket;
        std::uint32_t acknowledgedPawn;
        std::uint32_t playerCameraManager;
        std::uint32_t cameraCachePrivate;
        std::uint32_t pov;
        std::uint32_t location;
        std::uint32_t rotation;
        std::uint32_t fov;
        std::uint32_t mesh;
        std::uint32_t rootComponent;
        std::uint32_t relativeLocation;
        std::uint32_t componentVelocity;
    };
}
