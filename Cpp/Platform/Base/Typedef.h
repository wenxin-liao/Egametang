#ifndef BASE_TYPEDEFS_H
#define BASE_TYPEDEFS_H

#include <memory>
#include <boost/cstdint.hpp>
#include <google/protobuf/service.h>
#include <google/protobuf/message.h>

namespace Egametang {

typedef boost::int8_t   int8;
typedef boost::int16_t  int16;
typedef boost::int32_t  int32;
typedef boost::int64_t  int64;
typedef boost::uint8_t  uint8;
typedef boost::uint16_t uint16;
typedef boost::uint32_t uint32;
typedef boost::uint64_t uint64;

// smart_ptr typedef

typedef std::shared_ptr<int> IntPtr;
typedef std::shared_ptr<std::string> StringPtr;

// google
typedef std::shared_ptr<google::protobuf::Service> ProtobufServicePtr;
typedef std::shared_ptr<google::protobuf::Message> ProtobufMessagePtr;

} // namespace Egametang

#endif // BASE_TYPEDEFS_H
