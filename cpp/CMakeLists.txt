cmake_minimum_required(VERSION 3.17)
project(oktasample)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_BUILD_TYPE DEBUG)


include(FetchContent)

FetchContent_Declare(json
        GIT_REPOSITORY  https://github.com/ArthurSonzogni/nlohmann_json_cmake_fetchcontent
        GIT_TAG v3.7.3)

FetchContent_GetProperties(json)
if(NOT json_POPULATED)
    FetchContent_Populate(json)
    add_subdirectory(${json_SOURCE_DIR} ${json_BINARY_DIR} EXCLUDE_FROM_ALL)
endif()




add_executable(oktasample oktauth.cpp)
target_link_libraries(oktasample PRIVATE nlohmann_json::nlohmann_json -L/home/julianguarin/Code/cpp/cpr-build/lib cpr curl-d z mongoose /usr/lib/x86_64-linux-gnu/libssl.a
        /usr/lib/x86_64-linux-gnu/libcrypto.a -lpthread -ldl)

