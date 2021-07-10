#include <cpr/cpr.h>
#include <iostream>
#include <nlohmann/json.hpp>

using json = nlohmann::json;


int main(int argc, char ** argv)
{
    std::cout << "Basic Authentication test sample with OKTA" << std::endl;

    if (argc < 3)
    {
        std::cout << "Usage: auth username password\n";
        return -1;
    }

    auto data = json{};
    data["username"] = argv[1];
    data["password"] = argv[2];

    std::string datastring = data.dump();

    cpr::Response r = cpr::Post(
            cpr::Url{"https://wbiegames.okta.com/api/v1/authn"},
            cpr::Body{datastring},
            cpr::Header{
                {"Accept", "application/json"},
                {"Content-Type", "application/json"}
            }
            );


    std::cout << "STATUS CODE : " << r.status_code << std::endl;                  // 200
    std::cout << "+++++++++++++++++++++++++++++++++++++++++" << std::endl;
    std::cout << std::endl << "CONTENT TYPE: " << r.header["content-type"] << std::endl;       // application/json; charset=utf-8
    std::cout << "+++++++++++++++++++++++++++++++++++++++++" << std::endl;
    std::cout << r.text << std::endl;
    return 0;

}
